using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Encounter Data", menuName = "Encounter/ Data", order = 0)]
public class EncounterData : ScriptableObject
{
    [Header("Player Encounter Data")]
    public List<GameObject> PlayerDeck; //for storing player deck status
    public List<GameObject> PlayerTableCards; //for storing player deck status

    [Header("Enemy Encounter Data")]
    public List<GameObject> EnemyDeck; //for storing enemy deck status
    public List<GameObject> EnemyTableCards; //for storing enemy deck status
    [Header("Neutral Encounter Data")]
    public int JokerCardsCollected; // for storing total number of joker cards currently collected

    private void OnDisable()
    {
        PlayerDeck.Clear();
        PlayerTableCards.Clear();
        EnemyDeck.Clear();
        EnemyTableCards.Clear();
        JokerCardsCollected = 0;
    }
    public void ClearAllData()
    {
        PlayerDeck.Clear();
        PlayerTableCards.Clear();
        EnemyDeck.Clear();
        EnemyTableCards.Clear();
        JokerCardsCollected = 0;
    }
}
