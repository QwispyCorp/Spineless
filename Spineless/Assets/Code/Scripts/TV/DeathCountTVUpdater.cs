using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathCountTVUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cardCountText;
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

        //cardCountText.SetText("Death cards on table: " + Environment.NewLine + " " + totalDeathCards);
    }
}
