using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Camera item reveals all of player's card values (safe, death, or joker)
public class CameraItem : MonoBehaviour, Interactable
{
    [SerializeField] private PlayerSaveData saveData;
    public void Interact()
    {
        // flip all player cards here
    }
}
