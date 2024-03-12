using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathCountTVUpdater : MonoBehaviour
{
    [SerializeField] private GameObject[] numberObjects;
    private GameObject lastNumber;
    [SerializeField] private EncounterData encounterData;
    void OnEnable()
    {
        EnemyDeckLogic.EnemyCardsUpdated += UpdateScreen;
        PlayerDeckLogic.PlayerCardsUpdated += UpdateScreen;
    }
    void OnDisable()
    {
        EnemyDeckLogic.EnemyCardsUpdated -= UpdateScreen;
        PlayerDeckLogic.PlayerCardsUpdated -= UpdateScreen;
    }

    void UpdateScreen()
    {
        int playerDeathCards = 0;
        int enemyDeathCards = 0;

        for (int i = 0; i < encounterData.PlayerTableCards.Count; i++)
        {
            if (encounterData.PlayerTableCards[i])
            {
                if (encounterData.PlayerTableCards[i].name.Contains("Death"))
                {
                    playerDeathCards++;
                }
            }
        }

        for (int i = 0; i < encounterData.EnemyTableCards.Count; i++)
        {
            if (encounterData.EnemyTableCards[i])
            {
                if (encounterData.EnemyTableCards[i].name.Contains("Death"))
                {
                    enemyDeathCards++;
                }
            }

        }

        int totalDeathCards = playerDeathCards + enemyDeathCards;

        if (lastNumber != null)
        {
            lastNumber.SetActive(false);
        }
        switch (totalDeathCards)
        {
            case 0:
                //turn on number 0
                numberObjects[0].SetActive(true);
                lastNumber = numberObjects[0];
                break;
            case 1:
                //turn on number 1
                numberObjects[1].SetActive(true);
                lastNumber = numberObjects[1];
                break;
            case 2:
                //turn on number 2
                numberObjects[2].SetActive(true);
                lastNumber = numberObjects[2];
                break;
            case 3:
                //turn on number 3
                numberObjects[3].SetActive(true);
                lastNumber = numberObjects[3];
                break;
            case 4:
                //turn on number 4
                numberObjects[4].SetActive(true);
                lastNumber = numberObjects[4];
                break;
            case 5:
                //turn on number 5
                numberObjects[5].SetActive(true);
                lastNumber = numberObjects[5];
                break;
            case 6:
                //turn on number 6
                numberObjects[6].SetActive(true);
                lastNumber = numberObjects[6];
                break;
            case 7:
                //turn on number 7
                numberObjects[7].SetActive(true);
                lastNumber = numberObjects[7];
                break;
            case 8:
                //turn on number 8
                numberObjects[8].SetActive(true);
                lastNumber = numberObjects[8];
                break;
            case 9:
                //turn on number 9
                numberObjects[9].SetActive(true);
                lastNumber = numberObjects[9];
                break;
            case 10:
                //turn on number 10
                numberObjects[10].SetActive(true);
                lastNumber = numberObjects[10];
                break;
            default:
                break;
        }

        //cardCountText.SetText("Death cards on table: " + Environment.NewLine + " " + totalDeathCards);
    }
}
