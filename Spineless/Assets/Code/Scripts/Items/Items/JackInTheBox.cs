using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JackInTheBox : MonoBehaviour, Interactable
{
    private GameObject enemyDeckObject; // Reference to the EnemyDeck GameObject

    void Start()
    {
        enemyDeckObject = GameObject.Find("Enemy Deck");
    }

    public void Interact()
    {
        if (SceneManager.GetActiveScene().name == "Encounter")
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
            gameObject.SetActive(false);
        }
    }
}
