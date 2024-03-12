using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private Item itemSO;
    public IntegerReference playerHealth;
    public IntegerReference maxPlayerHealth;
    string _itemName;
    private GameObject playerDeckObject; // Reference to the Player Deck GameObject
    private GameObject enemyDeckObject; // Reference to the EnemyDeck GameObject

    private void OnEnable()
    {
        ItemAnimationDelay.OnItemAnimationEnded += ExecuteEffect;
    }
    private void OnDisable()
    {
        ItemAnimationDelay.OnItemAnimationEnded -= ExecuteEffect;
    }

    void Start()
    {
        _itemName = itemSO.itemName;
        playerDeckObject = GameObject.Find("Player Deck");
        enemyDeckObject = GameObject.Find("Enemy Deck");
    }
    void ExecuteEffect()
    {
        switch (_itemName)
        {
            case "Camera":
                Debug.Log(_itemName + " Effect Used");
                CameraEffect();
                break;
            case "Eye Of Foresight":
                Debug.Log(_itemName + " Effect Used");
                EyeEffect();
                break;
            case "Holy Cross":
                Debug.Log(_itemName + " Effect Used");
                HolyCrossEffect();
                break;
            case "Jack In The Box":
                Debug.Log(_itemName + " Effect Used");
                JackEffect();
                break;
            case "Pocket Knife":
                PocketKnifeEffect();
                Debug.Log(_itemName + " Effect Used");
                break;
            case "Matchbox":
                MatchBoxEffect();
                Debug.Log(_itemName + " Effect Used");
                break;
            case "Severed Hand":
                SeveredHandEffect();
                Debug.Log(_itemName + " Effect Used");
                break;
            case "Snake":
                SnakeEffect();
                Debug.Log(_itemName + " Effect Used");
                break;
            case "Stapler":
                StaplerEffect();
                Debug.Log(_itemName + " Effect Used");
                break;
            case "Wishbone":
                WishboneEffect();
                Debug.Log(_itemName + " Effect Used");
                break;
            default:
                Debug.LogWarning("EFFECT NOT EXECUTED");
                break;
        }
        if (_itemName != "Pocket Knife" && _itemName != "Eye Of Foresight")
        {
            StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.PlayerTurn);
        }
        Destroy(gameObject);
    }

    void CameraEffect()
    {
        if (playerDeckObject != null)
        {
            // Get the EnemyDeckLogic component from the GameObject
            PlayerDeckLogic playerDeckLogic = playerDeckObject.GetComponent<PlayerDeckLogic>();

            if (playerDeckLogic != null)
            {
                AudioManager.Instance.PlaySound("HolyCross");
                playerDeckLogic.ShowAllCards();
            }
            else
            {
                Debug.LogError("PlayerDeckLogic component not found on the specified GameObject.");
            }
        }
        else
        {
            Debug.LogError("No Player Deck found");
        }
    }
    void EyeEffect()
    {
        StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.PlayerEye);
    }
    void HolyCrossEffect()
    {
        if (playerDeckObject != null)
        {
            // Get the EnemyDeckLogic component from the GameObject
            PlayerDeckLogic playerDeckLogic = playerDeckObject.GetComponent<PlayerDeckLogic>();

            if (playerDeckLogic != null)
            {
                AudioManager.Instance.PlaySound("HolyCross");
                playerDeckLogic.RemoveDeathCard();
            }
            else
            {
                Debug.LogError("PlayerDeckLogic component not found on the specified GameObject.");
            }
        }
        else
        {
            Debug.LogError("No Player Deck found");
        }
    }
    void JackEffect()
    {
        if (enemyDeckObject != null)
        {
            // Get the EnemyDeckLogic component from the GameObject
            EnemyDeckLogic enemyDeckLogic = enemyDeckObject.GetComponent<EnemyDeckLogic>();

            if (enemyDeckLogic != null)
            {
                AudioManager.Instance.PlaySound("Jack In The Box");
                enemyDeckLogic.RefreshTable();
                enemyDeckLogic.DrawHand();
                Debug.Log("New Enemy Cards");
            }
            else
            {
                Debug.LogError("EnemyDeckLogic component not found on the specified GameObject.");
            }
        }
    }
    void MatchBoxEffect()
    {
        playerDeckObject.GetComponent<PlayerDeckLogic>().RefreshTable();
        playerDeckObject.GetComponent<PlayerDeckLogic>().DrawHand();
    }
    void PocketKnifeEffect()
    {
        StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.PlayerKnife);
    }
    void SeveredHandEffect()
    {
        if (playerDeckObject != null)
        {
            // Get the EnemyDeckLogic component from the GameObject
            PlayerDeckLogic playerDeckLogic = playerDeckObject.GetComponent<PlayerDeckLogic>();

            if (playerDeckLogic != null)
            {
                AudioManager.Instance.PlaySound("Severed Hand");
                playerDeckLogic.RefreshTable();
                playerDeckLogic.DrawHand();
                Debug.Log("New Player Cards");
            }
            else
            {
                Debug.LogError("PlayerDeckLogic component not found on the specified GameObject.");
            }
        }
    }
    void SnakeEffect()
    {
        if (enemyDeckObject != null)
        {
            // Get the EnemyDeckLogic component from the GameObject
            EnemyDeckLogic enemyDeckLogic = enemyDeckObject.GetComponent<EnemyDeckLogic>();

            if (enemyDeckLogic != null)
            {
                Debug.Log("Running snake effect");
                enemyDeckLogic.ShowAllCards();
            }
            else
            {
                Debug.LogError("EnemyDeckLogic component not found on the specified GameObject.");
            }
        }
        else
        {
            Debug.LogError("No Enemy Deck found");
        }
    }
    void StaplerEffect()
    {
        if (playerHealth.Value < maxPlayerHealth.Value) //ifplayer is under max health, add a finger
        {
            PlayerHealthTest.Instance.ChangeHealth(1);
            AudioManager.Instance.PlaySound("Stapler");
        }
        else //if player is at max health, still consume but break stapler
        {
            Debug.Log("Already at max fingers!");
            //Play broken SFX here
        }
    }
    void WishboneEffect()
    {
        StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.EnemyTurn);
    }
}
