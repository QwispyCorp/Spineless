using System.Collections;
using System.Collections.Generic;
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
    private GameObject _itemShopDescriptionObject;
    private GameObject _spawnedText;
    private GameObject ekgCanvas;
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
        _itemShopDescriptionObject = itemSO.itemShopDescription;

        buttonTextImage = buttonTextObject.GetComponent<UnityEngine.UI.Image>();
        buttonValueImage = buttonValueObject.GetComponent<UnityEngine.UI.Image>();


        //Find the item's corresponding text object in the scene
        if (GameObject.Find("EKG Canvas") != null)
        {
            Debug.Log("EKG Canvas located for " + _itemName);
            ekgCanvas = GameObject.Find("EKG Canvas");

            _spawnedText = Instantiate(_itemShopDescriptionObject, ekgCanvas.transform, false); //spawn the description onto the ekg canvas
            _spawnedText.SetActive(false); //turn it off

        }
        else
        {
            ekgCanvas = null;
            Debug.LogWarning("Could not find EKG Canvas.");
        }
    }
    void OnMouseDown() //when item is clicked on
    {
        //check if player has enough currency
        if (saveData.monsterFingers >= _itemValue) //if they have enough fingers to buy item
        {
            if (_spawnedText)
            {
                Destroy(_spawnedText); //Destroy the text description on canvas
            }

            PurchaseItem(); //purchase the item
        }
        else //if player doesn't have enough fingers
        {
            //play error sound?
            //not enough currency feedback
            AudioManager.Instance.PlaySound("Error");
            buttonTextImage.color = insufficientFingersColor; //highlight text red
            buttonValueImage.color = insufficientFingersColor; //highlight text red
            Debug.Log("Not enough fingers for " + _itemName + "!");
        }
    }
    void OnMouseUp() //after item is clicked on
    {
        if (buttonTextImage.color == insufficientFingersColor)
        {
            buttonTextImage.color = baseColor;//change color to normal when player unclicks after an unsuccessful purchase
            buttonValueImage.color = baseColor; //change color to normal when player unclicks after an unsuccessful purchase
        }
    }

    void OnMouseEnter() //when player hovers over button
    {
        buttonTextImage.color = highlightColor; //highlight text
        buttonValueImage.color = highlightColor; //highlight number

        if (_spawnedText)
        {
            _spawnedText.SetActive(true); //turn on item text
        }
    }

    void OnMouseExit() //when player leaves the button hover area
    {
        buttonTextImage.color = baseColor; //highlight text
        buttonValueImage.color = baseColor; //highlight number
        if (_spawnedText)
        {
            _spawnedText.SetActive(false); //turn off item text
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
        Destroy(_spawnedText);
        Destroy(gameObject); //destroy object
    }
}
