using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EquippedItemSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    [SerializeField] private PlayerSaveData saveData;
    void Start()
    {
        for (int i = 0; i < saveData.EquippedItems.Count; i++)
        {
            Debug.Log("Equipped Items Count: " + saveData.EquippedItems.Count);
            //spawn objects
            if (saveData.EquippedItems.Count > 0)
            {
                Debug.Log("Spawning object " + saveData.EquippedItems[i].itemName);
                Instantiate(saveData.EquippedItems[i].itemPrefab, spawnPoints[i], false);
            }
        }
    }
}
