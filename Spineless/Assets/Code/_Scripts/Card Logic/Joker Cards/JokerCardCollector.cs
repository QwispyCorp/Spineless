using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JokerCardCollector : MonoBehaviour
{
    [SerializeField] private GameObject jokerCardPrefab;
    [SerializeField] private EncounterData encounterData;
    [SerializeField] private Transform[] jokerSpawnPoints;
    [SerializeField] private Material jokerMaterial;
    public delegate void JokerStart();
    public static event JokerStart OnJokerStart;


    void OnEnable()
    {
        PlayerCardInteraction.OnJokerFlipped += AddJokerToTable;
        PlayerCardInteraction.OnJokerExecutionCompleted += ClearJokerCards; //for checking execution on player cards by player
        EnemyCardInteraction.OnJokerExecutionCompleted2 += ClearJokerCards; //for checking execution on enemy cards by player
    }

    void OnDisable()
    {
        PlayerCardInteraction.OnJokerFlipped -= AddJokerToTable;
        PlayerCardInteraction.OnJokerExecutionCompleted -= ClearJokerCards;
        EnemyCardInteraction.OnJokerExecutionCompleted2 -= ClearJokerCards; //for checking execution on enemy cards by player
    }

    void AddJokerToTable()
    {
        //spawn joker card at next available slot on table
        for (int i = 0; i < jokerSpawnPoints.Length; i++)
        {
            if (jokerSpawnPoints[i].childCount == 0) //check if currently indexed joker slot is available to spawn joker card
            {
                GameObject newJokerCard = Instantiate(jokerCardPrefab, jokerSpawnPoints[i], false); //display joker card on table
                newJokerCard.GetComponent<MeshRenderer>().material = jokerMaterial; //assign the card the joker material
                encounterData.JokerCardsCollected++; //incremenet joker cards collected in encounter data container
                break; //break out to avoid duplicate spawning
            }
        }

        if (encounterData.JokerCardsCollected == 3) //if max number of joker cards have been reached
        {
            if (StateManager.Instance.CurrentEncounterState == StateManager.EncounterState.PlayerSafe) //if it's currently the player's turn
            {
                if (OnJokerStart != null)
                {
                    OnJokerStart?.Invoke();
                }
                AudioManager.Instance.PlaySound("Joker");
                StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.PlayerJokerExecution); //enter player execution state
            }
            else if (StateManager.Instance.CurrentEncounterState == StateManager.EncounterState.EnemyTurn) //if it's currntly the enemy's turn
            {
                StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.EnemyJokerExecution); //enter enemy execution state
            }
        }
    }
    void ClearJokerCards()
    {
        if (encounterData.JokerCardsCollected == 3)
        {
            for (int i = 0; i < jokerSpawnPoints.Length; i++)
            {
                if (jokerSpawnPoints[i].GetChild(0).gameObject)
                {
                    Destroy(jokerSpawnPoints[i].GetChild(0).gameObject);
                }
            }
            encounterData.JokerCardsCollected = 0;
        }
    }
}
