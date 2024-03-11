using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Save Data", menuName = "Player/Save Data", order = 0)]

public class PlayerSaveData : ScriptableObject
{
    [Header("Save Attributes")]
    public int TargetEncounterWins;
    public int EncountersWon; //for storing number of encounters player has won
    public int EncountersCleared; // for storing total number of encouters cleared
    public bool ShopVisited; //for checking if player can enter the shop 
    public bool FirstEncounterEntered; //for playing tutorial in first encounter

    [Header("Game Board Data")]
    public bool BoardGenerated; //for keeping track of board generation state
    public GameObject GameBoard; //for storing generated game board
    public float TileSpacing;
    [Header("Item Data")]
    public List<Item> MasterItemPool; //for storing all available items
    public List<Item> Inventory; //for storing unlocked items
    public List<Item> EquippedItems; //for storing equipped items
    [Header("Resources")]
    public int playerFingersInNextEncounter;
    public int monsterFingers; //for storing the monster fingers player has collected

    private void OnDisable()
    { // reset all data on disable
        ClearAllData();
    }

    public void ClearAllData()
    {
        //Inventory.Clear();
        //EquippedItems.Clear();
        BoardGenerated = false;
        FirstEncounterEntered = false;
        ShopVisited = false;
        GameBoard = null;
        EncountersWon = 0;
        EncountersCleared = 0;
        playerFingersInNextEncounter = 5;
        monsterFingers = 0;
    }
}
