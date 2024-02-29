using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    private static StateManager _instance;
    public static StateManager Instance { get { return _instance; } }
    public EncounterState CurrentEncounterState;
    [SerializeField] private EnemyDeckLogic enemyDeck;
    [SerializeField] private PlayerDeckLogic playerDeck;
    [SerializeField] private float enemyTurnTime;

    void Awake()
    {
        //on awake check for existence of manager and handle accordingly
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        //entry of tutorial state on encounters cleared check goes here
        UpdateEncounterState(EncounterState.PlayerTurn);
    }
    public void ForceEnemyTurn()
    {
        if (CurrentEncounterState == EncounterState.EnemyTurn)
        {
            // If it's already enemy's turn, run the enemy AI immediately
            RunEnemyCardAI();
        }
        else
        {
            // If it's not enemy's turn, switch to enemy's turn state
            UpdateEncounterState(EncounterState.EnemyTurn);
        }
    }
    public void UpdateEncounterState(EncounterState newState)
    {
        CurrentEncounterState = newState;

        switch (newState)
        {
            case EncounterState.PlayerTurn:
                HandlePlayerTurn();
                break;
            case EncounterState.PlayerDamage:
                HandlePlayerDamage();
                break;
            case EncounterState.PlayerJokerExecution:
                HandlePlayerJoker();
                break;
            case EncounterState.PlayerSafe:
                HandlePlayerSafe();
                break;
            case EncounterState.EnemyTurn:
                HandleEnemyTurn();
                break;
            case EncounterState.EnemyDamage:
                HandleEnemyDamage();
                break;
            case EncounterState.EnemySafe:
                HandleEnemySafe();
                break;
            case EncounterState.EnemyJokerExecution:
                HandleEnemyJoker();
                break;
        }
    }

    private void HandlePlayerTurn()
    {
        Debug.Log("PLAYER CURRENTLY IN: Player Turn State");
        if (playerDeck.CheckTableCards() == 0)
        { //if hand is empty at beginning of turn, draw hand
            playerDeck.DrawHand();
        }
    }
    private void HandlePlayerDamage()
    {
        Debug.Log("PLAYER CURRENTLY IN: Player Damage State");
        Debug.Log("Switching to Enemy turn...");
        UpdateEncounterState(EncounterState.EnemyTurn);
    }
    private void HandlePlayerSafe()
    {
        Debug.Log("PLAYER CURRENTLY IN: Player Safe State.");
    }
    private void HandleEnemyTurn()
    {
        //Check if table is empty
        if (enemyDeck.CheckTableCards() == 0)
        {
            //if table is empty at beginning of enemy turn, redraw all cards
            enemyDeck.DrawHand();
        }
        Debug.Log("ENEMY CURRENTLY IN: Enemy Turn State");
        Invoke("RunEnemyCardAI", enemyTurnTime);
    }
    private void HandleEnemyDamage()
    {
        Debug.Log("ENEMY CURRENTLY IN: Enemy Damage State");
        Debug.Log("Switching to player turn...");
    }
    private void HandleEnemySafe()
    {
        Debug.Log("ENEMY CURRENTLY IN: Enemy Safe State");
        Debug.Log("Switching to player turn...");
    }

    private void HandleEnemyJoker()
    {
        Debug.Log("ENEMY CURRENTLY IN: Enemy Joker State");
        Invoke("RunEnemyJokerAI", enemyTurnTime);
    }
    private void HandlePlayerJoker()
    {
        Debug.Log("PLAYER CURRENTLY IN: Player Joker State");
    }
    private void RunEnemyCardAI()
    {
        enemyDeck.EnemyCardSelection();
    }
    private void RunEnemyJokerAI()
    {
        enemyDeck.EnemyJokerExecution();
    }

    public enum EncounterState
    {
        PlayerTurn,
        PlayerDamage,
        PlayerJokerExecution,
        PlayerSafe,
        EnemyTurn,
        EnemyDamage,
        EnemySafe,
        EnemyJokerExecution,
        EncounterTutorial
    }
}
