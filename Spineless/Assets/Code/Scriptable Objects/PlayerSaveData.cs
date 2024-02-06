using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Save Data", menuName = "Player/Save Data", order = 0)]

public class PlayerSaveData : ScriptableObject
{
    [Header("Encounter Data")]
    public List<GameObject> Deck; //for storing player deck status
    public List<GameObject> TableCards; //for storing player deck status
    public int EncountersWon; //for storing number of encounters player has won

    [Header("Game Board Data")]
    public bool BoardGenerated; //for keeping track of board generation state
    public GameObject GameBoard; //for storing generated game board
    [Header("Inventory Data")]
    public List<Item> ItemPool; //for storing equipped items
    public List<Item> Inventory; //for storing collected items
    public List<Item> EquippedItems; //for storing equipped items

    private void OnDisable() { // reset all data on disable
        Deck.Clear();
        TableCards.Clear();
        Inventory.Clear();
        EquippedItems.Clear();
        BoardGenerated = false;
        GameBoard = null;
        EncountersWon = 0;
    }
}
