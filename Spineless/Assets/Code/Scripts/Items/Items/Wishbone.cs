using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Wishbone item makes the enemy take another turn (force an enemy turn)
public class Wishbone : MonoBehaviour, Interactable
{
    public void Interact()
    {
        //switch to item used state
        //play wishbone animation
        //activate effect after item animation plays
        StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.EnemyTurn);
        gameObject.SetActive(false);
        
    }
}
