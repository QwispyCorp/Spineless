using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Save Data", menuName = "Player/Save Data", order = 0)]

public class PlayerSaveData : ScriptableObject
{
    public int EncountersWon; //for storing number of encounters player has won

    [Header("Game Board Data")]
    public bool BoardGenerated; //for keeping track of board generation state
    public GameObject GameBoard; //for storing generated game board
    public float tileSpacing;
    [Header("Item Data")]
    public List<Item> MasterItemPool; //for storing all available items
    public List<Item> Inventory; //for storing unlocked items
    public List<Item> EquippedItems; //for storing equipped items
    [Header("Currency")]
    public int monsterFingers; //for storing the monster fingers player has collected

    private void OnDisable()
    { // reset all data on disable
        //Inventory.Clear();
        EquippedItems.Clear();
        BoardGenerated = false;
        GameBoard = null;
        EncountersWon = 0;
    }
}
