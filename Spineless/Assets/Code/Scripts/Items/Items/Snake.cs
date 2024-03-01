using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Snake item revelas all of the enemy's cards in hand without executing their effects
public class Snake : MonoBehaviour, Interactable
{
    [SerializeField] private PlayerSaveData saveData;
    private GameObject enemyDeckObject; // Reference to the Player Deck GameObject

    void Start()
    {
        enemyDeckObject = GameObject.Find("Enemy Deck");
    }
    public void Interact()
    {
        // flip all enemy cards here
        if (SceneManager.GetActiveScene().name == "Encounter") //Interact Logic for item in Encounter room: Perform item effect and consume by de-activating game object
        {
            if (enemyDeckObject != null)
            {
                // Get the EnemyDeckLogic component from the GameObject
                EnemyDeckLogic enemyDeckLogic = enemyDeckObject.GetComponent<EnemyDeckLogic>();

                if (enemyDeckLogic != null)
                {
                    AudioManager.Instance.PlaySound("HolyCross");
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
            gameObject.SetActive(false);
        }
    }
}
