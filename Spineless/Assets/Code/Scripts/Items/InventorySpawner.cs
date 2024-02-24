using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    [SerializeField] private PlayerSaveData saveData;
    void Start()
    {
        for (int i = 0; i < saveData.Inventory.Count; i++)
        {
            Debug.Log("Equipped Items Count: " + saveData.Inventory.Count);
            //spawn objects
            if (saveData.Inventory.Count > 0)
            {
                Debug.Log("Spawning object " + saveData.Inventory[i].itemName);
                GameObject spawnedItem = Instantiate(saveData.Inventory[i].itemPrefab, spawnPoints[i].transform, false);
            }
            else
            {
                Debug.LogError("Nothing in player inventory!");
            }
        }
    }
}
