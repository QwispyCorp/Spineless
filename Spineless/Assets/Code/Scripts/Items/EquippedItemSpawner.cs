using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EquippedItemSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
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
                Instantiate(saveData.EquippedItems[i].itemPrefab, new Vector3(spawnPoints[i].position.x, saveData.EquippedItems[i].itemPrefab.gameObject.transform.position.y, spawnPoints[i].position.z), Quaternion.identity, spawnPoints[i]);
            }
        }
    }
}
