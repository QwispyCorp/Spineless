using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTileLightColor : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private Color openEscapeColor;
    [SerializeField] private Color closedEscapeColor;

    void OnEnable()
    {
        if (saveData.EncountersWon >= saveData.TargetEncounterWins)
        {

            GetComponent<Light>().color = openEscapeColor;
        }
        else
        {

            GetComponent<Light>().color = closedEscapeColor;
        }

    }
}
