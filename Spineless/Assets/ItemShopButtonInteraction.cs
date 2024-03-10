using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopButtonInteraction : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;

    private GameObject itemTextObject;
    private string _itemName;
    private string _itemDescription;
    private int _itemValue;
    private GameObject _itemPrefab;
    private GameObject _itemShopButton;
    [SerializeField] private Item itemSO;
    [SerializeField] private GameObject buttonTextObject;
    private UnityEngine.UI.Image buttonTextImage;
    [SerializeField] private GameObject buttonValueObject;
    private UnityEngine.UI.Image buttonValueImage;
    [Header("Hover Settings")]
    [SerializeField] private Color highlightColor;
    [SerializeField] private Color purchaseColor;
    [SerializeField] private Color insufficientFingersColor;
    private Color baseColor = new Color(255, 255, 255);
    public delegate void ItemPurchased();
    public static event ItemPurchased OnItemPurchased;

    void Awake()
    {

        _itemName = itemSO.itemName;
        _itemValue = itemSO.value;
        _itemPrefab = itemSO.itemPrefab;
        _itemDescription = itemSO.itemDescription;
        _itemShopButton = itemSO.itemShopButton;

        buttonTextImage = buttonTextObject.GetComponent<UnityEngine.UI.Image>();
        buttonValueImage = buttonValueObject.GetComponent<UnityEngine.UI.Image>();


        //Find the item's corresponding text object in the scene
        if (GameObject.Find(_itemName + " Text") != null)
        {
            Debug.Log(_itemName + " Text Object Found for ItemShopButtonInteraction.");
            itemTextObject = GameObject.Find(_itemName + " Text");
            itemTextObject.SetActive(false);
        }
        else
        {
            itemTextObject = null;
            Debug.LogWarning("Could not find text object for " + _itemName + "in ItemShopButtonInteraction..");
        }
    }
    void OnMouseDown()
    {
        //check if player has enough currency
        if (saveData.monsterFingers >= _itemValue)
        {
            if (itemTextObject)
            {
                itemTextObject.SetActive(false); //turn off the item text description
            }

            PurchaseItem(); //purchase the item
        }
        else
        {
            //play error sound?
            //not enough currency feedback
            buttonTextImage.color = insufficientFingersColor;
            buttonValueImage.color = insufficientFingersColor;
            Debug.Log("Not enough fingers for " + _itemName + "!");
        }
    }

    void OnMouseEnter()
    {
        buttonTextImage.color = highlightColor;
        buttonValueImage.color = highlightColor;

        if (itemTextObject)
        {
            itemTextObject.SetActive(true); //turn on item text
        }
    }

    void OnMouseExit()
    {
        buttonTextImage.color = baseColor;
        buttonValueImage.color = baseColor;
        if (itemTextObject)
        {
            itemTextObject.SetActive(false); //turn on item text
        }
    }
    private void PurchaseItem()
    {
        buttonTextImage.color = purchaseColor;

        saveData.monsterFingers -= _itemValue; //subtract currency from player
        if (OnItemPurchased != null)
        {
            OnItemPurchased?.Invoke(); //trigger item purchased event to update monster fingers in finger jar UI
        }
        if (saveData.EquippedItems.Count == 4) //if equipped items is full, store item in inventory
        {
            saveData.Inventory.Add(itemSO); //add item to inventory
        }
        else
        {
            saveData.EquippedItems.Add(itemSO);
        }
        //play item sound effect
        //update monster finger jar count/ text
        Destroy(gameObject); //destroy object
    }
}
