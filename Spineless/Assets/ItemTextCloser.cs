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
            if (currentScene == "GameBoard") //in geame board room, turn off item tv text for items that aren't equipped and are also not in inventory
            {
                if (!saveData.Inventory.Find(x => x.itemName == item.itemName) && !saveData.EquippedItems.Find(x => x.itemName == item.itemName)) //if the item is not in inventoiry AND is not in equipped items, turn off its screen in this room
                {
                    //Find the item's corresponding text object in the scene
                    if (GameObject.Find(item.itemName + " Text") != null)
                    {
                        Debug.LogWarning(item.itemName + " Text Object Turned Off");
                        GameObject itemTextObject = GameObject.Find(item.itemName + " Text");
                        itemTextObject.SetActive(false);
                    }
                    else
                    {
                        Debug.LogWarning("Could not find text object for " + item.itemName + ".");
                    }
                }
            }
            if (currentScene == "Encounter") //in encounter room, turn off item tv text for items not currently equipped
            {
                if (!saveData.EquippedItems.Find(x => x.itemName == item.itemName)) //if the item is not in equipped items, turn off its screen in this room
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
            }
        }
    }
}
