using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TVTextLoader : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;

    void Start()
    {
        //ENCOUNTER FUNCTIONALITY 
        if (SceneManager.GetActiveScene().name == "Encounter")
        {
            for (int i = 0; i < saveData.MasterItemPool.Count; i++)
            {
                if (saveData.EquippedItems.Find(x => x.itemName == saveData.MasterItemPool[i].itemName)) //if equipped items contains the currently indexed item in the item pool, keep its text object active
                {
                    continue;
                }
                else
                {
                    if (GameObject.Find(saveData.MasterItemPool[i].itemName + " Text") != null)
                    {
                        GameObject.Find(saveData.MasterItemPool[i].itemName + " Text").SetActive(false);
                    }
                }
            }
        }
        else
        {
            if (GameObject.Find("Death Card Text") != null)
            {
                GameObject.Find("Death Card Text").SetActive(false);
            }
        }

        if (SceneManager.GetActiveScene().name == "ItemRoom")
        {
            if (GameObject.Find("Item Room Text") != null)
            {
                GameObject.Find("Item Room Text").SetActive(true);
            }
        }
    }
}
