using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemMouseInteraction : MonoBehaviour
{
    private string itemName;
    private string itemDescription;
    private int itemValue;
    [SerializeField] private Item itemSO;
    [SerializeField] private PlayerSaveData saveData;
    private MeshRenderer mesh;
    public Color hoverEmissionColor;
    public float hoverEmissionIntensity;
    private GameObject itemTextObject;
    private GameObject encounterTVTextObject;
    private GameObject itemRoomTVTextObject;
    private GameObject shopRoomTVTextObject;
    private GameObject gameBoardRoomTVTextObject;
    private GameObject cabinetItems;
    private Transform[] trayTransforms;
    private Transform[] cabinetTransforms;
    private string currentRoom;
    public delegate void ItemPurchased();
    public static event ItemPurchased OnItemPurchased;

    void Awake()
    {
        currentRoom = SceneManager.GetActiveScene().name;
        itemName = itemSO.itemName;
        itemValue = itemSO.value;
        itemDescription = itemSO.itemDescription;

        //Find the item's corresponding text object in the scene
        if (GameObject.Find(itemName + " Text") != null)
        {
            itemTextObject = GameObject.Find(itemName + " Text");
        }
        else
        {
            itemTextObject = null;
            Debug.LogWarning("Could not find text object for " + itemName + ".");
        }

        //ITEM INTERACTION SETUP FOR ENCOUNTER ROOM -------------
        if (currentRoom == "Encounter")
        {
            //Turn on encounter tv text
            if (GameObject.Find("Death Card Text") != null)
            {
                encounterTVTextObject = GameObject.Find("Death Card Text");
                encounterTVTextObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Could not find TV Death Card Text object.");
            }

            //turn off item room tv text
            if (GameObject.Find("Item Room Text") != null)
            {
                itemRoomTVTextObject = GameObject.Find("Item Room Text");
                itemRoomTVTextObject.SetActive(false);
            }
            //turn off shop room tv text
            if (GameObject.Find("Shop Room Text") != null)
            {
                itemRoomTVTextObject = GameObject.Find("Shop Room Text");
                itemRoomTVTextObject.SetActive(false);
            }
            if (GameObject.Find("Game Board Room Text") != null)
            {
                gameBoardRoomTVTextObject = GameObject.Find("Game Board Room Text");
                gameBoardRoomTVTextObject.SetActive(false);
            }
        }

        //ITEM INTERACTION SETUP FOR GAME BOARD ROOM -------------
        if (currentRoom == "GameBoard")
        {
            //Find the transforms for equipping/ unequipping items
            if (GameObject.Find("Cabinet Items") != null)
            {
                cabinetItems = GameObject.Find("Cabinet Items");
                trayTransforms = cabinetItems.GetComponent<EquippedItemSpawner>().spawnPoints;
                cabinetTransforms = cabinetItems.GetComponent<InventorySpawner>().spawnPoints;
            }
            else
            {
                Debug.LogWarning("Could not find Cabinet Items object.");
            }

            //turn off encounter tv text 
            if (GameObject.Find("Death Card Text") != null)
            {
                encounterTVTextObject = GameObject.Find("Death Card Text");
                encounterTVTextObject.SetActive(false);
            }
            //turn off item room tv text
            if (GameObject.Find("Item Room Text") != null)
            {
                itemRoomTVTextObject = GameObject.Find("Item Room Text");
                itemRoomTVTextObject.SetActive(false);
            }
            //turn off shop room tv text
            if (GameObject.Find("Shop Room Text") != null)
            {
                itemRoomTVTextObject = GameObject.Find("Shop Room Text");
                itemRoomTVTextObject.SetActive(false);
            }
            if (GameObject.Find("Game Board Room Text") != null)
            {
                gameBoardRoomTVTextObject = GameObject.Find("Game Board Room Text");
                gameBoardRoomTVTextObject.SetActive(true);
            }
        }

        //ITEM INTERACTION SETUP FOR ITEM ROOM --------------------
        if (currentRoom == "ItemRoom")
        {
            //Assign item room tv text
            if (GameObject.Find("Item Room Text") != null)
            {
                itemRoomTVTextObject = GameObject.Find("Item Room Text");
                itemRoomTVTextObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Could not find Item Room TV Text object.");
            }

            //turn off encounter tv text
            if (GameObject.Find("Death Card Text") != null)
            {
                encounterTVTextObject = GameObject.Find("Death Card Text");
                encounterTVTextObject.SetActive(false);
            }
            //turn off shop room text
            if (GameObject.Find("Shop Room Text") != null)
            {
                encounterTVTextObject = GameObject.Find("Shop Room Text");
                encounterTVTextObject.SetActive(false);
            }
            if (GameObject.Find("Game Board Room Text") != null)
            {
                gameBoardRoomTVTextObject = GameObject.Find("Game Board Room Text");
                gameBoardRoomTVTextObject.SetActive(false);
            }
        }
        //ITEM INTERACTION SETUP FOR SHOP ROOM --------------------
        if (currentRoom == "ShopRoom")
        {
            //turn on shop room tv text
            if (GameObject.Find("Shop Room Text") != null)
            {
                shopRoomTVTextObject = GameObject.Find("Shop Room Text");
                shopRoomTVTextObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Could not find Shop Room TV Text object.");
            }
            //turn off encounter room tv text
            if (GameObject.Find("Death Card Text") != null)
            {
                encounterTVTextObject = GameObject.Find("Death Card Text");
                encounterTVTextObject.SetActive(false);
            }
            //turn off item room tv text
            if (GameObject.Find("Death Card Text") != null)
            {
                encounterTVTextObject = GameObject.Find("Death Card Text");
                encounterTVTextObject.SetActive(false);
            }
            if (GameObject.Find("Game Board Room Text") != null)
            {
                gameBoardRoomTVTextObject = GameObject.Find("Game Board Room Text");
                gameBoardRoomTVTextObject.SetActive(false);
            }
        }
        if (GetComponent<MeshRenderer>() != null)
        {
            mesh = GetComponent<MeshRenderer>();
        }
    }

    void Start()
    {

    }
    //-------------------------------------WHEN PLAYER HOVERS OVER ITEM WITH CURSOR 
    private void OnMouseEnter()
    {
        if (mesh != null) //if the gameobject has a mmesh renderer, highlight its mesh
        {
            mesh.material.SetColor("_EmissiveColor", hoverEmissionColor * hoverEmissionIntensity); //highlight item

            if (transform.childCount > 0) //if the item has children, check for mesh renderers in those objects
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).GetComponent<MeshRenderer>()) //if the child has a mesh renderer
                    {
                        transform.GetChild(i).GetComponent<MeshRenderer>().material.SetColor("_EmissiveColor", hoverEmissionColor * hoverEmissionIntensity); //highlight item
                    }
                    if (transform.GetChild(i).GetComponent<SkinnedMeshRenderer>()) //if the child has a skinned mesh renderer
                    {
                        transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissiveColor", hoverEmissionColor * hoverEmissionIntensity); //highlight item
                    }
                }
            }
        }
        else //otherwise cycle through children to highlight all different parts of mesh
        {
            Debug.Log(itemName + " object child count: " + transform.childCount);
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<MeshRenderer>() != null) //if the child has a mesh renderer
                {
                    transform.GetChild(i).GetComponent<MeshRenderer>().material.SetColor("_EmissiveColor", hoverEmissionColor * hoverEmissionIntensity); //highlight item
                }
                if (transform.GetChild(i).GetComponent<SkinnedMeshRenderer>()) //if the child has a skinned mesh renderer
                {
                    transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissiveColor", hoverEmissionColor * hoverEmissionIntensity); //highlight item
                }
            }
        }

        if (itemTextObject)
        {
            itemTextObject.SetActive(true); //turn on item text
            if (currentRoom == "ItemRoom")
            {
                itemTextObject.GetComponent<TextMeshProUGUI>().SetText(itemName + ": " + Environment.NewLine + itemDescription + Environment.NewLine + "Collect?"); //update item text object
            }
            else if (currentRoom == "ShopRoom")
            {
                itemTextObject.GetComponent<TextMeshProUGUI>().SetText(itemName + ": " + Environment.NewLine + itemDescription + Environment.NewLine + "Finger Cost: " + itemValue); //update item text object
            }
            else if (currentRoom == "Encounter")
            {
                itemTextObject.GetComponent<TextMeshProUGUI>().SetText(itemName + ": " + Environment.NewLine + itemDescription); //update item text object
            }
        }
        if (encounterTVTextObject)
        {
            encounterTVTextObject.SetActive(false); //turn off default tv text when hovering over item
        }
        if (itemRoomTVTextObject)
        {
            itemRoomTVTextObject.SetActive(false); //turn off item room tv prompt
        }
        if (shopRoomTVTextObject)
        {
            shopRoomTVTextObject.SetActive(false); //turn off shop room tv prompt
        }
        if (gameBoardRoomTVTextObject)
        {
            gameBoardRoomTVTextObject.SetActive(false); //turn off game board room tv prompt
        }
    }
    //-------------------------------------WHEN PLAYER EXITS ITEM WITH CURSOR 
    private void OnMouseExit()
    {
        if (mesh != null) //if the gameobject has a mesh renderer
        {
            mesh.material.SetColor("_EmissiveColor", Color.black);
        }
        else //otherwise cycle through children to highlight all different parts of mesh
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<MeshRenderer>()) //if the child has a mesh renderer
                {
                    transform.GetChild(i).GetComponent<MeshRenderer>().material.SetColor("_EmissiveColor", Color.black);
                }
                if (transform.GetChild(i).GetComponent<SkinnedMeshRenderer>()) //if the child has a skinned mesh renderer
                {
                    transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissiveColor", Color.black); //highlight item
                }
            }
        }
        
        if (itemTextObject)
        {
            itemTextObject.SetActive(false); //turn off item text object
        }
        if (currentRoom == "Encounter" && encounterTVTextObject)
        {
            encounterTVTextObject.SetActive(true); //turn on default tv text object
        }
        if (currentRoom == "ItemRoom" && itemRoomTVTextObject)
        {
            itemRoomTVTextObject.SetActive(true); //turn on item room tv prompt
        }
        if (currentRoom == "ShopRoom" && shopRoomTVTextObject)
        {
            shopRoomTVTextObject.SetActive(true); //turn on shop room tv prompt
        }
        if (currentRoom == "GameBoard" && gameBoardRoomTVTextObject)
        {
            gameBoardRoomTVTextObject.SetActive(true); //turn on game board room tv prompt
        }
        
    }
    //-------------------------------------WHEN PLAYER CLICKS ON ITEM WITH CURSOR 
    private void OnMouseDown()
    {

        //ITEM FUNCTIONALITY FOR GAME BOARD ROOM -----------------------------------------------------------
        if (currentRoom == "GameBoard") //if in game board room, equip or unequip item
        {
            if (itemTextObject)
            {
                itemTextObject.SetActive(false); //turn off the item text description
            }
            if (encounterTVTextObject)
            {
                encounterTVTextObject.SetActive(true); //turn on the tv text
            }
            if (saveData.EquippedItems.Exists(x => x.name == itemName)) //if item is equipped in tray, unequip it and move to cabinet
            {
                UnequipItem();
            }
            else if (saveData.Inventory.Exists(x => x.name == itemName)) //if item is in the player's cabinet, equip it and move it to tray
            {
                EquipItem();
            }
        }

        //ITEM FUNCTIONALITY FOR ITEM ROOM ----------------------------------
        if (currentRoom == "ItemRoom")
        {
            if (itemTextObject)
            {
                itemTextObject.SetActive(false); //turn off the item text description
            }
            CollectItem(); //collect the item
            AudioManager.Instance.PlaySound(itemName);//play the item's sound effect during transition
            //turn off HUD all elements
            LightManager.Instance.StartFlickeringTransitionTo("GameBoard"); //switch back to game board room
        }
        //ITEM FUNCTIONALITY FOR SHOP ROOM ---------------------------------
        if (currentRoom == "ShopRoom")
        {
            //check if player has enough currency
            if (saveData.monsterFingers >= itemValue)
            {
                if (itemTextObject)
                {
                    itemTextObject.SetActive(false); //turn off the item text description
                }
                if (shopRoomTVTextObject)
                {
                    shopRoomTVTextObject.SetActive(true); //turn on the tv text
                }

                PurchaseItem(); //purchase the item
            }
            else
            {
                //play error sound?
                //not enough currency feedback
                Debug.Log("Not enough fingers for " + itemName + "!");
            }
        }
    }
    // ------------------------------------ PRIVATE UTIL FUNCTIONS --------------

    //GAME BOARD ROOM FUNCTIONS
    private void UnequipItem()
    {
        saveData.Inventory.Add(saveData.EquippedItems.Find(x => x.name == itemName)); // add item to inventory
                                                                                      //spawn item in cabinet slot:
        for (int i = 0; i < cabinetTransforms.Length; i++)
        {
            if (cabinetTransforms[i].transform.childCount == 0)//if the current transform has no children, therefore does not have an object spawned on it
            {
                Instantiate(saveData.EquippedItems.Find(x => x.name == itemName).itemPrefab, cabinetTransforms[i].transform.position, Quaternion.identity, cabinetTransforms[i]);//spawn object at this spawn point
                break; //break out of loop once the item is spawned to prevent duplicate spawns 
            }
        }
        saveData.EquippedItems.Remove(saveData.EquippedItems.Find(x => x.name == itemName)); //remove item from inventory
        Destroy(gameObject); //destroy the tray item object
    }
    private void EquipItem()
    {
        if (saveData.EquippedItems.Count < 3) //if equipped tray is not full, add item to equipped list, remove item from inventory, and move item to equipped tray 
        {
            saveData.EquippedItems.Add(saveData.Inventory.Find(x => x.name == itemName)); //add item to equipped items list
                                                                                          //spawn item in tray:
            for (int i = 0; i < trayTransforms.Length; i++)
            {
                if (trayTransforms[i].transform.childCount == 0)//if the current transform has no children, therefore does not have an object spawned on it
                {
                    Instantiate(saveData.Inventory.Find(x => x.name == itemName).itemPrefab, trayTransforms[i].transform.position, Quaternion.identity, trayTransforms[i]);//spawn object at this spawn point
                    break; //break out of loop once the item is spawned to prevent duplicate spawns 
                }
            }
            saveData.Inventory.Remove(saveData.Inventory.Find(x => x.name == itemName)); //remove item from inventory
            Destroy(gameObject); //destroy the current item
        }
        else if (saveData.EquippedItems.Count == 3) //if equipped tray is full
        {
            Debug.Log("Tray Full!"); //communicate tray's full
        }
    }

    // ITEM ROOM FUNCTIONS
    private void PurchaseItem()
    {
        saveData.monsterFingers -= itemValue; //subtract currency from player
        if (OnItemPurchased != null)
        {
            OnItemPurchased?.Invoke(); //trigger item purchased event to update monster fingers in finger jar UI
        }
        //play item sound effect
        saveData.Inventory.Add(itemSO); //add item to inventory
        //update monster finger jar count/ text
        Destroy(gameObject); //destroy object
    }
    private void CollectItem()
    {
        saveData.Inventory.Add(itemSO);
        Destroy(gameObject);
    }

}
