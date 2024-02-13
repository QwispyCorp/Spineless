using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemMouseInteraction : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private PlayerSaveData saveData;
    private MeshRenderer mesh;
    public Color hoverEmissionColor;
    public float hoverEmissionIntensity;
    private GameObject itemText;
    private GameObject cabinetItems;
    private Transform[] trayTransforms;
    private Transform[] cabinetTransforms;

    void Start()
    {
        //Find the item's corresponding text object in the scene
        if (GameObject.Find(itemName + " Text") != null)
        {
            itemText = GameObject.Find(itemName + " Text");
            itemText.SetActive(false);
        }
        else
        {
            itemText = null;
            Debug.Log("Could not find text object for " + itemName + ".");
        }
        //Find the transforms for equipping/ unequipping items
        if (GameObject.Find("Cabinet Items") != null)
        {
            cabinetItems = GameObject.Find("Cabinet Items");
            trayTransforms = cabinetItems.GetComponent<EquippedItemSpawner>().spawnPoints;
            cabinetTransforms = cabinetItems.GetComponent<InventorySpawner>().spawnPoints;
        }
        else
        {
            Debug.Log("Could not find Cabinet Items object.");
        }
        mesh = GetComponent<MeshRenderer>();
    }
    private void OnMouseEnter()
    {
        mesh.material.SetColor("_Emissive", hoverEmissionColor * hoverEmissionIntensity);
        if (itemText)
        {
            itemText.SetActive(true);
        }
    }
    private void OnMouseExit()
    {
        mesh.material.SetColor("_Emissive", Color.black);
        if (itemText)
        {
            itemText.SetActive(false);
        }
    }
    private void OnMouseDown()
    {

        if (itemText)
        {
            itemText.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "GameBoard")
        {
            if (saveData.EquippedItems.Exists(x => x.name == itemName)) //if item is equipped in tray, unequip it and move to cabinet
            {
                UnequipItem();
            }
            else if (saveData.Inventory.Exists(x => x.name == itemName)) //if item is in the player's cabinet, equip it and move it to tray
            {
                EquipItem();
            }
        }
    }
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
            Debug.Log("Tray Full!");
            //communicate tray's full
        }
    }
}
