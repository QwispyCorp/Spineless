using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVTextLoader : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;

    void Start()
    {
        for (int i = 0; i < saveData.MasterItemPool.Count; i++)
        {
            if (saveData.EquippedItems.Find(x => x.itemName == saveData.MasterItemPool[i].itemName)) //if equipped items contains the currently indexed item in the item pool, keep its text object active
            {
                continue;
            }
            else
            {
                GameObject.Find(saveData.MasterItemPool[i].itemName + " Text").SetActive(false);
            }
        }
    }
}
