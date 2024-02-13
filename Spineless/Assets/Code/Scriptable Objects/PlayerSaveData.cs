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
    public float tileSpacing;
    [Header("Inventory Data")]
    public List<Item> MasterItemPool; //for storing all available items
    public List<Item> PlayerItemPool; //for storing all available items in current game
    public List<Item> Inventory; //for storing unlocked items
    public List<Item> EquippedItems; //for storing equipped items

    private void OnEnable()
    {
        //Transfer all items in master item pool to player item pool
        for (int i = 0; i < MasterItemPool.Count; i++)
        {
            PlayerItemPool.Add(MasterItemPool[i]);
        }
    }

    private void OnDisable()
    { // reset all data on disable
        Deck.Clear();
        PlayerItemPool.Clear();
        TableCards.Clear();
        //Inventory.Clear();
        EquippedItems.Clear();
        BoardGenerated = false;
        GameBoard = null;
        EncountersWon = 0;
    }
}
