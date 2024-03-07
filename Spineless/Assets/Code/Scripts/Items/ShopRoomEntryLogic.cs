using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopRoomEntryLogic : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private Transform leftSpawn;
    [SerializeField] private Transform rightSpawn;
    void Start()
    {
        //when player enters shop, update shop visisted for shopo availability on board
        saveData.ShopVisited = true;

        //When shop room starts, spawn random items in the spawn points for player purchasing

        //Spawn left item,  store which item index was used
        int leftItemIndex = UnityEngine.Random.Range(0, saveData.MasterItemPool.Count);
        GameObject leftItem = Instantiate(saveData.MasterItemPool[leftItemIndex].itemPrefab, leftSpawn, false);

        //Spawn right item, checking for non-duplicate item from the left's index
        int rightItemIndex;
        while (true)
        {
            rightItemIndex = UnityEngine.Random.Range(0, saveData.MasterItemPool.Count);
            if (rightItemIndex != leftItemIndex)
            {
                GameObject rightItem = Instantiate(saveData.MasterItemPool[rightItemIndex].itemPrefab, rightSpawn, false);
                break;
            }
        }

        //When item room starts, turn off all item text on tv, turn on item room prompt
        for (int i = 0; i < saveData.MasterItemPool.Count; i++)
        {
            if (GameObject.Find(saveData.MasterItemPool[i].itemName) == null)
            {
                Debug.Log(saveData.MasterItemPool[i].itemName);
                if (GameObject.Find(saveData.MasterItemPool[i].itemName + " Text") != null)
                {
                    GameObject.Find(saveData.MasterItemPool[i].itemName + " Text").SetActive(false);
                }
            }
        }
    }
}
