using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Save Data", menuName = "Player/Save Data", order = 0)]

public class PlayerSaveData : ScriptableObject
{
    public List<Item> Inventory; //for storing items
    public GameObject GameBoard; //for storing generated game board
    public int encountersWon; //for funsies

}
