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
    private GameObject tvTextObject;
    private GameObject cabinetItems;
    private Transform[] trayTransforms;
    private Transform[] cabinetTransforms;

    void Start()
    {
        itemName = itemSO.itemName;

        //Find the item's corresponding text object in the scene
        if (GameObject.Find(itemName + " Text") != null)
        {
            itemTextObject = GameObject.Find(itemName + " Text");
            itemTextObject.SetActive(false);
        }
        else
        {
            itemTextObject = null;
            Debug.LogWarning("Could not find text object for " + itemName + ".");
        }
        //Assign tv text
        if (GameObject.Find("Death Card Text") != null)
        {
            tvTextObject = GameObject.Find("Death Card Text");
            tvTextObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Could not find TV Death Card Text object.");
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
            Debug.LogWarning("Could not find Cabinet Items object.");
        }
        mesh = GetComponent<MeshRenderer>();
    }
    private void OnMouseEnter()
    {
        if (mesh != null) //if the gameobject has a mmesh renderer
        {
            mesh.material.SetColor("_Emissive", hoverEmissionColor * hoverEmissionIntensity);
        }
        else //otherwise cycle through children to highlight all different parts of mesh
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<MeshRenderer>()) //if the child has a mesh renderer
                {
                    mesh.material.SetColor("_Emissive", hoverEmissionColor * hoverEmissionIntensity);
                }
            }
        }

        if (itemTextObject)
        {
            itemTextObject.SetActive(true); //turn on item text
            itemTextObject.GetComponent<TextMeshProUGUI>().SetText(itemDescription); //update item text object
        }
        if (tvTextObject)
        {
            tvTextObject.SetActive(false); //turn off tv text
        }
    }
    private void OnMouseExit()
    {
        if (mesh != null) //if the gameobject has a mesh renderer
        {
            mesh.material.SetColor("_Emissive", Color.black);
        }
        else //otherwise cycle through children to highlight all different parts of mesh
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<MeshRenderer>()) //if the child has a mesh renderer
                {
                    mesh.material.SetColor("_Emissive", Color.black);
                }
            }
        }
        if (itemTextObject)
        {
            itemTextObject.SetActive(false); //turn off item text object
        }
        if (tvTextObject)
        {
            tvTextObject.SetActive(true); //turn on default tv text object
        }
    }
    private void OnMouseDown()
    {

        if (itemTextObject)
        {
            itemTextObject.SetActive(false); //turn off the item text description
        }
        if (tvTextObject)
        {
            tvTextObject.SetActive(true); //turn on the tv text
        }

        if (SceneManager.GetActiveScene().name == "GameBoard") //if in game board room, equip or unequip item
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
