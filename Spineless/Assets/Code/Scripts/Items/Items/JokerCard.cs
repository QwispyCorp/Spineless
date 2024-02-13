using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JokerCard : MonoBehaviour, Interactable
{
    private GameObject enemyDeckObject; // Reference to the EnemyDeck GameObject

    void Start()
    {
        enemyDeckObject = GameObject.Find("Enemy Deck");
    }

    public void Interact()
    {
        if (SceneManager.GetActiveScene().name == "Prototype")
        {
            if (enemyDeckObject != null)
            {
                // Get the EnemyDeckLogic component from the GameObject
                EnemyDeckLogic enemyDeckLogic = enemyDeckObject.GetComponent<EnemyDeckLogic>();

                if (enemyDeckLogic != null)
                {
                    AudioManager.Instance.PlaySound("Joker");
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
