using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script that loads in/ spawns items for the player choice in the item room

public class ItemRoomItemSpawner : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private Transform leftSpawn;
    [SerializeField] private Transform rightSpawn;
    void Start()
    {
        //When item room starts, spawn random items in the spawn points for player choice

        //Spawn left item,  store which item index was used
        int leftItemIndex = UnityEngine.Random.Range(0, saveData.MasterItemPool.Count);
        int rightItemIndex;
        GameObject leftItem = Instantiate(saveData.MasterItemPool[leftItemIndex].itemPrefab, leftSpawn.transform.position, Quaternion.identity, leftSpawn);

        //Spawn right item, checking for non-duplicate item from the left's index
        while (true)
        {
            rightItemIndex = UnityEngine.Random.Range(0, saveData.MasterItemPool.Count);
            if (rightItemIndex != leftItemIndex)
            {
                GameObject rightItem = Instantiate(saveData.MasterItemPool[rightItemIndex].itemPrefab, rightSpawn.transform.position, Quaternion.identity, leftSpawn);
                break;
            }
        }

        //When item room starts, turn off all item text on tv, turn on item room prompt
        // for (int i = 0; i < saveData.MasterItemPool.Count; i++)
        // {
        //     if (GameObject.Find(saveData.MasterItemPool[i].itemName + " Text") != null)
        //     {
        //         GameObject.Find(saveData.MasterItemPool[i].itemName + " Text").SetActive(false);
        //     }
        // }

    }
}
