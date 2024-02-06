using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVTextLoader : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;

    void Start()
    {
        for (int i = 0; i < saveData.ItemPool.Count; i++)
        {
            if (saveData.EquippedItems.Find(x => x.itemName == saveData.ItemPool[i].itemName)) //if equipped items contains the currently indexed item in the item pool, keep its text object active
            {
                continue;
            }
            else
            {
                GameObject.Find(saveData.ItemPool[i].itemName + " Text").SetActive(false);
            }
        }
    }
}
