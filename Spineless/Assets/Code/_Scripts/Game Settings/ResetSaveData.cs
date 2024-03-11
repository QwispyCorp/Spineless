using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResetSaveData : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    public void ResetSaveDataFunc()
    {
        saveData.Inventory.Clear();
        saveData.EquippedItems.Clear();
        saveData.EncountersWon = 0;
        saveData.BoardGenerated = false;
    }
}
