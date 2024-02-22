using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterRoomStartup : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    void Start()
    {
        saveData.ShopVisited = false; //every time player enters encounter room, reset shop availability
    }
}
