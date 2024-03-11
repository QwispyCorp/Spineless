using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//severed hand item redraws all of player's hand
public class SeveredHand : MonoBehaviour
{

    private GameObject playerDeckObject; // Reference to the EnemyDeck GameObject

    void Start()
    {
        playerDeckObject = GameObject.Find("Player Deck");
    }
    public void Interact()
    {
        if (SceneManager.GetActiveScene().name == "Encounter")
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
            gameObject.SetActive(false);
        }
    }
}
