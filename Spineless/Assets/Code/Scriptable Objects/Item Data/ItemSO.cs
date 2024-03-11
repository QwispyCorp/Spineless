using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 0)]
[System.Serializable]
public class Item : ScriptableObject
{
    public string itemName;
    [TextAreaAttribute(6,20)]
    public string itemDescription;
    public int value;
    public GameObject itemPrefab;
    public GameObject itemShopButton;
    public GameObject itemShopDescription;
}
