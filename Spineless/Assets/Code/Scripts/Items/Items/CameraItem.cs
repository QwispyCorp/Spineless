using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Camera item reveals all of player's card values (safe, death, or joker)
public class CameraItem : MonoBehaviour, Interactable
{
    [SerializeField] private PlayerSaveData saveData;
    private GameObject playerDeckObject; // Reference to the Player Deck GameObject

    void Start()
    {
        playerDeckObject = GameObject.Find("Player Deck");
        //Debug.Log("Camera item set up, Player Deck Logic: " + playerDeckObject);
    }
    public void Interact()
    {
        // flip all player cards here
        if (SceneManager.GetActiveScene().name == "Encounter") //Interact Logic for item in Encounter room: Perform item effect and consume by de-activating game object
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
            gameObject.SetActive(false);
        }
    }
}
