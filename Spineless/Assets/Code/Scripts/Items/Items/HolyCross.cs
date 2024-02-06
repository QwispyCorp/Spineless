using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyCross : MonoBehaviour, Interactable
{
    private GameObject playerDeckObject; // Reference to the Player Deck GameObject

    void Start()
    {
        playerDeckObject = GameObject.Find("Player Deck");
    }

    public void Interact()
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
            Debug.LogError("EnemyDeck GameObject reference is not assigned.");
        }
        gameObject.SetActive(false);
    }
}
