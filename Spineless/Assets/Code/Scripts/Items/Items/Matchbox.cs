using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//Matchbox item burns/ destroys all unrevealed cards in the player's hand
public class Matchbox : MonoBehaviour, Interactable
{
    private GameObject playerDeckObject;
    private bool isUsed;
    [SerializeField] private EncounterData encounterData;

    void Start()
    {
        playerDeckObject = GameObject.Find("Player Deck");
        isUsed = false;
        //Debug.Log("Camera item set up, Player Deck Logic: " + playerDeckObject);
    }
    public void Interact()
    {
        if (SceneManager.GetActiveScene().name == "Encounter" && isUsed == false)
        {
            isUsed = true;
            for (int i = 0; i < encounterData.PlayerTableCards.Count; i++)
            {
                playerDeckObject.GetComponent<PlayerDeckLogic>().RefreshTable();
            }

            StartCoroutine("BurnDelay");
        }
    }

    private IEnumerator BurnDelay()
    {
        yield return new WaitForSeconds(2); //animation delay
        playerDeckObject.GetComponent<PlayerDeckLogic>().DrawHand();
        gameObject.SetActive(false);
    }
}
