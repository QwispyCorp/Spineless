using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Encounter Data", menuName = "Encounter/ Data", order = 0)]
public class EncounterData : ScriptableObject
{
    [Header("Encounter Data")]
    public List<GameObject> PlayerDeck; //for storing player deck status
    public List<GameObject> PlayerTableCards; //for storing player deck status

    public List<GameObject> EnemyDeck; //for storing enemy deck status
    public List<GameObject> EnemyTableCards; //for storing enemy deck status

    private void OnDisable() {
        PlayerDeck.Clear();
        PlayerTableCards.Clear();
        EnemyDeck.Clear();
        EnemyTableCards.Clear();
    }
}
