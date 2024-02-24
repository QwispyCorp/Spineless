using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//very ugly tv text loader 
public class TVTextLoader : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    private string currentRoom;

    void Awake()
    {
        currentRoom = SceneManager.GetActiveScene().name;
    }
    void Start()
    {
        //ENCOUNTER FUNCTIONALITY 
        if (currentRoom == "Encounter")
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
            if (GameObject.Find("Item Room Text") != null)
            {
                GameObject.Find("Item Room Text").SetActive(false);
            }
            if (GameObject.Find("Death Card Text") != null)
            {
                GameObject.Find("Death Card Text").SetActive(true);
            }
            if (GameObject.Find("Shop Room Text") != null)
            {
                GameObject.Find("Shop Room Text").SetActive(false);
            }
            if (GameObject.Find("Game Board Room Text") != null)
            {
                GameObject.Find("Game Board Room Text").SetActive(false);
            }
        }
        //GAME BOARD ROOM FUNCTIONALITY
        if (currentRoom == "GameBoard")
        {
            if(GameObject.Find("Video Screen") != null){
                GameObject.Find("Video Screen").SetActive(false);
            }
            if (GameObject.Find("Game Board Room Text") != null)
            {
                GameObject.Find("Game Board Room Text").SetActive(false);
            }

            if (GameObject.Find("Shop Room Text") != null)
            {
                GameObject.Find("Shop Room Text").SetActive(false);
            }
            if (GameObject.Find("Death Card Text") != null)
            {
                GameObject.Find("Death Card Text").SetActive(false);
            }
            if (GameObject.Find("Item Room Text") != null)
            {
                GameObject.Find("Item Room Text").SetActive(false);
            }
        }
        //ITEM ROOM FUNCTIONALITY
        if (currentRoom == "ItemRoom")
        {
            if (GameObject.Find("Item Room Text") != null)
            {
                GameObject.Find("Item Room Text").SetActive(true);
            }
            if (GameObject.Find("Death Card Text") != null)
            {
                GameObject.Find("Death Card Text").SetActive(false);
            }
            if (GameObject.Find("Shop Room Text") != null)
            {
                GameObject.Find("Shop Room Text").SetActive(false);
            }
            if (GameObject.Find("Game Board Room Text") != null)
            {
                GameObject.Find("Game Board Room Text").SetActive(false);
            }
        }
        //SHOP ROOM FUNCTIONALITY
        if (currentRoom == "ShopRoom")
        {
            if (GameObject.Find("Shop Room Text") != null)
            {
                GameObject.Find("Shop Room Text").SetActive(true);
            }
            if (GameObject.Find("Death Card Text") != null)
            {
                GameObject.Find("Death Card Text").SetActive(false);
            }
            if (GameObject.Find("Item Room Text") != null)
            {
                GameObject.Find("Item Room Text").SetActive(false);
            }
            if (GameObject.Find("Game Board Room Text") != null)
            {
                GameObject.Find("Game Board Room Text").SetActive(false);
            }
        }
    }
}