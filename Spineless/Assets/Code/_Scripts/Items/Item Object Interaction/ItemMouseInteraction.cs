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
    private GameObject itemPrefab;
    [SerializeField] private Item itemSO;
    [SerializeField] private PlayerSaveData saveData;
    private MeshRenderer mesh;
    public Color hoverEmissionColor;
    public float hoverEmissionIntensity;
    [HideInInspector] public GameObject itemTextObject;
    private GameObject encounterTVTextObject;
    private GameObject itemRoomTVTextObject;
    private GameObject shopRoomTVTextObject;
    private GameObject gameBoardRoomTVTextObject;
    private GameObject cabinetItems;
    private Transform[] trayTransforms;
    private Transform[] cabinetTransforms;
    private string currentRoom;
    private GameObject itemRoomSpawnPoint1;
    private GameObject itemRoomSpawnPoint2;

    void Awake()
    {
        encounterTVTextObject = GameObject.Find("Death Card Text");
        itemRoomTVTextObject = GameObject.Find("Item Room Text");
        shopRoomTVTextObject = GameObject.Find("Shop Room Text");
        gameBoardRoomTVTextObject = GameObject.Find("Game Board Room Text");


        currentRoom = SceneManager.GetActiveScene().name;
        itemName = itemSO.itemName;
        itemValue = itemSO.value;
        itemPrefab = itemSO.itemPrefab;
        itemDescription = itemSO.itemDescription;

        //Find the item's corresponding text object in the scene
        if (GameObject.Find(itemName + " Text") != null)
        {
            Debug.Log(itemName + " Text Object Found");
            itemTextObject = GameObject.Find(itemName + " Text");
            itemTextObject.SetActive(false);
        }
        else
        {
            itemTextObject = null;
            Debug.LogWarning("Could not find text object for " + itemName + ".");
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
        }

        //ITEM INTERACTION SETUP FOR ITEM ROOM --------------------
        if (currentRoom == "ItemRoom")
        {
            itemRoomSpawnPoint1 = GameObject.Find("Spawn Point 1");
            itemRoomSpawnPoint2 = GameObject.Find("Spawn Point 2");
        }
        if (GetComponent<MeshRenderer>() != null)
        {
            mesh = GetComponent<MeshRenderer>();
        }
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
                        foreach (Material mat in transform.GetChild(i).GetComponent<MeshRenderer>().materials)
                        {
                            mat.SetColor("_EmissiveColor", hoverEmissionColor * hoverEmissionIntensity); //highlight item
                        }
                    }
                    if (transform.GetChild(i).GetComponent<SkinnedMeshRenderer>()) //if the child has a skinned mesh renderer
                    {
                        foreach (Material mat in transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().materials)
                        {
                            mat.SetColor("_EmissiveColor", hoverEmissionColor * hoverEmissionIntensity); //highlight item
                        }
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
                    foreach (Material mat in transform.GetChild(i).GetComponent<MeshRenderer>().materials)
                    {
                        mat.SetColor("_EmissiveColor", hoverEmissionColor * hoverEmissionIntensity); //highlight item
                    }
                }
                if (transform.GetChild(i).GetComponent<SkinnedMeshRenderer>()) //if the child has a skinned mesh renderer
                {
                    //transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissiveColor", hoverEmissionColor * hoverEmissionIntensity); //highlight item
                    foreach (Material mat in transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().materials)
                    {
                        mat.SetColor("_EmissiveColor", hoverEmissionColor * hoverEmissionIntensity); //highlight item
                    }
                }
            }
        }

        if (itemTextObject)
        {
            itemTextObject.SetActive(true); //turn on item text
            if (currentRoom == "ShopRoom")
            {
                //itemTextObject.GetComponent<TextMeshProUGUI>().SetText(itemName + ": " + Environment.NewLine + itemDescription + Environment.NewLine + "Finger Cost: " + itemValue); //update item text object
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
                    foreach (Material mat in transform.GetChild(i).GetComponent<MeshRenderer>().materials)
                    {
                        mat.SetColor("_EmissiveColor", Color.black); //highlight item
                    }
                }
                if (transform.GetChild(i).GetComponent<SkinnedMeshRenderer>()) //if the child has a skinned mesh renderer
                {
                    foreach (Material mat in transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().materials)
                    {
                        mat.SetColor("_EmissiveColor", Color.black); //highlight item
                    }
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
    private void OnMouseUp()
    {
        //ITEM FUNCTIONALITY FOR ENCOUNTER ROOM -----------------------------------------------------------
        if (currentRoom == "Encounter") //if in encounter room, consume item
        {
            if (itemTextObject)
            {
                itemTextObject.SetActive(false); //turn off the item text description
            }
            if (encounterTVTextObject)
            {
                encounterTVTextObject.SetActive(true); //turn on the tv text
            }
            ConsumeItem();
        }

        //ITEM FUNCTIONALITY FOR GAME BOARD ROOM -----------------------------------------------------------
        if (currentRoom == "GameBoard") //if in game board room, equip or unequip item
        {
            if (itemTextObject)
            {
                itemTextObject.SetActive(false); //turn off the item text description
            }
            if (gameBoardRoomTVTextObject)
            {
                gameBoardRoomTVTextObject.SetActive(true); //turn on the tv text
            }
            if (saveData.EquippedItems.Exists(x => x.itemName == itemName)) //if item is equipped in tray, unequip it and move to cabinet
            {
                UnequipItem();
            }
            else if (saveData.Inventory.Exists(x => x.itemName == itemName)) //if item is in the player's cabinet, equip it and move it to tray
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
            itemRoomSpawnPoint1.SetActive(false);
            itemRoomSpawnPoint2.SetActive(false);
        }
    }
    // ------------------------------------ PRIVATE UTIL FUNCTIONS --------------

    //GAME BOARD ROOM FUNCTIONS
    private void UnequipItem()
    {
        saveData.Inventory.Add(saveData.EquippedItems.Find(x => x.itemName == itemName)); // add item to inventory
                                                                                          //spawn item in cabinet slot:
        for (int i = 0; i < cabinetTransforms.Length; i++)
        {
            if (cabinetTransforms[i].transform.childCount == 0)//if the current transform has no children, therefore does not have an object spawned on it
            {
                //transform.position = cabinetTransforms[i].transform.position;//move item to inventory
                //transform.parent = cabinetTransforms[i].transform; //parent item to cabinet spot
                GameObject newItem = Instantiate(itemPrefab, cabinetTransforms[i], false); //spawn same item in new poisition
                newItem.GetComponent<ItemMouseInteraction>().itemTextObject = itemTextObject;
                Destroy(gameObject);//destroy current item
                break; //break out of loop once the item is spawned to prevent duplicate spawns 
            }
        }
        saveData.EquippedItems.Remove(saveData.EquippedItems.Find(x => x.itemName == itemName)); //remove item from inventory
    }
    private void EquipItem()
    {
        if (saveData.EquippedItems.Count < 4) //if equipped tray is not full, add item to equipped list, remove item from inventory, and move item to equipped tray 
        {
            saveData.EquippedItems.Add(saveData.Inventory.Find(x => x.itemName == itemName)); //add item to equipped items list

            //spawn item in tray:
            for (int i = 0; i < trayTransforms.Length; i++)
            {
                if (trayTransforms[i].transform.childCount == 0) //if the current transform has no children, therefore does not have an object spawned on it
                {
                    Debug.Log("Spawning item: " + itemName + ".");
                    //transform.position = trayTransforms[i].transform.position;//move item to tray
                    //transform.parent = trayTransforms[i].transform; //parent item to cabinet spot
                    //break; //break out of loop once the item is spawned to prevent duplicate spawns 
                    GameObject newItem = Instantiate(itemPrefab, trayTransforms[i], false); //spawn same item in new poisition
                    newItem.GetComponent<ItemMouseInteraction>().itemTextObject = itemTextObject;
                    Destroy(gameObject);//destroy current item
                    break;
                }
            }
            saveData.Inventory.Remove(saveData.Inventory.Find(x => x.itemName == itemName)); //remove item from inventory
        }
        else if (saveData.EquippedItems.Count == 4) //if equipped tray is full
        {
            Debug.Log("Tray Full!"); //communicate tray's full
        }
    }

    private void CollectItem()
    {
        if (saveData.EquippedItems.Count != 4)
        {
            saveData.EquippedItems.Add(itemSO);
        }
        else
        {
            saveData.Inventory.Add(itemSO);
        }
        Destroy(gameObject);
    }
    private void ConsumeItem()
    {
        saveData.EquippedItems.Remove(itemSO);
        Instantiate(itemSO.itemAnimationObject);
        Destroy(gameObject);
        //in encounter room, item is destroyed in its effect script
    }

}
