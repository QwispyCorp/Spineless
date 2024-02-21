using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

//Logic to unlock new random item to player's inventory

public class ItemRoomCollect : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private TextMeshProUGUI tvText;
    //upon loading into item room scene, 
    void Start()
    {
        if (saveData.MasterItemPool.Count != 0)
        {
            //Choose random item that player hasn't unlocked yet
            int randomItemIndex = UnityEngine.Random.Range(0, saveData.MasterItemPool.Count - 1);
            string newItemName = saveData.MasterItemPool[randomItemIndex].itemName;
            //Add item from player item pool to the player's inventory
            saveData.Inventory.Add(saveData.MasterItemPool[randomItemIndex]);
            //Remove item from player item pool 
            saveData.MasterItemPool.Remove(saveData.MasterItemPool[randomItemIndex]);
            //Change TV text to display item's name as unlocked
            tvText.SetText("UNLOCKED: " + Environment.NewLine + newItemName.ToUpper());
        }
        else
        {
            tvText.SetText("NO ITEMS LEFT. ");
        }
    }
}
