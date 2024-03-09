using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script that turns off item text canvases in the current room for the items that are not in the player's inventory or equipped items

public class ItemTextCloser : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;

    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        foreach (Item item in saveData.MasterItemPool)
        {
            if (currentScene == "GameBoard")
            {
                if (!saveData.Inventory.Find(x => x.name == item.itemName) && !saveData.EquippedItems.Find(x => x.name == item.itemName)) //if the item is not in inventoiry AND is not in equipped items, turn off its screen in this room
                {
                    //Find the item's corresponding text object in the scene
                    if (GameObject.Find(item.itemName + " Text") != null)
                    {
                        Debug.Log(item.itemName + " Text Object Found");
                        GameObject itemTextObject = GameObject.Find(item.itemName + " Text");
                        itemTextObject.SetActive(false);
                    }
                    else
                    {
                        Debug.LogWarning("Could not find text object for " + item.itemName + ".");
                    }
                }
                else if (!saveData.EquippedItems.Find(x => x.name == item.itemName)) //if the item is not in inventoiry AND is not in equipped items, turn off its screen in this room
                {
                    //Find the item's corresponding text object in the scene
                    if (GameObject.Find(item.itemName + " Text") != null)
                    {
                        Debug.Log(item.itemName + " Text Object Found");
                        GameObject itemTextObject = GameObject.Find(item.itemName + " Text");
                        itemTextObject.SetActive(false);
                    }
                    else
                    {
                        item.itemName = null;
                        Debug.LogWarning("Could not find text object for " + item.itemName + ".");
                    }
                }
                else if (!saveData.Inventory.Find(x => x.name == item.itemName)) //if the item is not in inventoiry AND is not in equipped items, turn off its screen in this room
                {
                    //Find the item's corresponding text object in the scene
                    if (GameObject.Find(item.itemName + " Text") != null)
                    {
                        Debug.Log(item.itemName + " Text Object Found");
                        GameObject itemTextObject = GameObject.Find(item.itemName + " Text");
                        itemTextObject.SetActive(false);
                    }
                    else
                    {
                        item.itemName = null;
                        Debug.LogWarning("Could not find text object for " + item.itemName + ".");
                    }
                }
            }
            if (currentScene == "Encounter")
            {
                if (!saveData.EquippedItems.Find(x => x.name == item.itemName)) //if the item is not in equipped items, turn off its screen in this room
                {
                    //Find the item's corresponding text object in the scene
                    if (GameObject.Find(item.itemName + " Text") != null)
                    {
                        Debug.Log(item.itemName + " Text Object Found");
                        GameObject itemTextObject = GameObject.Find(item.itemName + " Text");
                        itemTextObject.SetActive(false);
                    }
                    else
                    {
                        //item.itemName = null;
                        Debug.LogWarning("Could not find text object for " + item.itemName + ".");
                    }
                }
            }
        }
    }
}
