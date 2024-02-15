using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HolyCross : MonoBehaviour, Interactable
{
    private GameObject playerDeckObject; // Reference to the Player Deck GameObject
    [SerializeField] private PlayerSaveData saveData;

    void Start()
    {
        playerDeckObject = GameObject.Find("Player Deck");
        Debug.Log("Player Deck Logic: " + playerDeckObject);
    }

    public void Interact()
    {
        if (SceneManager.GetActiveScene().name == "Encounter") //Interact Logic for item in Encounter room: Perform item effect and consume by de-activating game object
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
            gameObject.SetActive(false);
        }
    }
}
