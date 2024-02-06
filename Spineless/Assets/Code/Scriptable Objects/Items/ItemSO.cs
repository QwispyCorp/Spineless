using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 0)]
[System.Serializable]
public class Item : ScriptableObject
{
    public string itemName;
    public int itemAmount;
    public GameObject itemPrefab;
}
