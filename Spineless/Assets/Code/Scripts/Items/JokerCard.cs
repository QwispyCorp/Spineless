using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerCard : MonoBehaviour, Interactable
{
    public GameObject enemyDeckObject; // Reference to the EnemyDeck GameObject

    public void Interact()
    {
        if (enemyDeckObject != null)
        {
            // Get the EnemyDeckLogic component from the GameObject
            EnemyDeckLogic enemyDeckLogic = enemyDeckObject.GetComponent<EnemyDeckLogic>();

            if (enemyDeckLogic != null)
            {
                enemyDeckLogic.RefreshTable();
                enemyDeckLogic.DrawHand();
                Debug.Log("New Enemey Cards");
            }
            else
            {
                Debug.LogError("EnemyDeckLogic component not found on the specified GameObject.");
            }
        }
        else
        {
            Debug.LogError("EnemyDeck GameObject reference is not assigned.");
        }
        gameObject.SetActive(false);    
    }
}
