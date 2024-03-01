using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Matchbox item burns/ destroys all unrevealed cards in the player's hand
public class Matchbox : MonoBehaviour, Interactable
{
    [SerializeField] private PlayerDeckLogic playerDeck;
    [SerializeField] private EncounterData encounterData;
    public void Interact()
    {
        if (SceneManager.GetActiveScene().name == "Encounter")
        {
            for (int i = 0; i < encounterData.PlayerTableCards.Count; i++)
            {
                if (!encounterData.PlayerTableCards[i].GetComponent<PlayerCardInteraction>().isClicked)
                {
                    playerDeck.RemoveCardFromTable(encounterData.PlayerTableCards[i]);
                }
            }

            StartCoroutine("BurnDelay");
        }
    }

    private IEnumerator BurnDelay()
    {
        yield return new WaitForSeconds(2); //animation delay
        playerDeck.DrawHand();
    }
}
