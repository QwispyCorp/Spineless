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

    public delegate void EnemyTurnStarted();
    public static event EnemyTurnStarted OnEnemyTurnStarted;


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
            case EncounterState.ItemUse:
                break;
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
        Invoke("DrawEnemyHand", 0.5f);
        Debug.Log("Going into: Player Turn State");
        Debug.Log("In Player Turn State");
    }
    private void HandlePlayerDamage()
    {
        Debug.Log("Going into: Player Damage State");
        Debug.Log("In Player Damage State");
    }
    private void HandlePlayerSafe()
    {
        Debug.Log("Going into: Player Safe State.");
        Debug.Log("In Player Safe State");
    }
    private void HandleEnemyTurn()
    {
        Invoke("DrawPlayerHand", 0.5f);
        Debug.Log("Going into: Enemy Turn State");
        Debug.Log("In Enemy Turn State");

        if (OnEnemyTurnStarted != null)
        {
            OnEnemyTurnStarted?.Invoke();
        }

        Invoke("RunEnemyCardAI", enemyTurnTime);
    }
    private void HandleEnemyDamage()
    {
        Debug.Log("Going into: Enemy Damage State");
        Debug.Log("In Enemy Damage State");
    }
    private void HandleEnemySafe()
    {
        Debug.Log("Going into: Enemy Safe State");
        Debug.Log("In Enemy Safe State");
    }

    private void HandleEnemyJoker()
    {
        Debug.Log("Going into: Enemy Joker State");
        Debug.Log("In Enemy Joker Execution State");
        Invoke("RunEnemyJokerAI", enemyTurnTime);
    }
    private void HandlePlayerJoker()
    {
        Debug.Log("Going into: Player Joker State");
        Debug.Log("In Player Joker Execution State");
    }
    private void RunEnemyCardAI()
    {
        enemyDeck.EnemyCardSelection();
    }
    private void RunEnemyJokerAI()
    {
        enemyDeck.EnemyJokerExecution();
    }

    private void DrawPlayerHand()
    {
        if (playerDeck.CheckTableCards() == 0) //redraw player hand if it's empty when the enemy turn starts
        {
            playerDeck.DrawHand();
        }
    }

    private void DrawEnemyHand()
    {
        if (enemyDeck.CheckTableCards() == 0) //redraw player hand if it's empty when the enemy turn starts
        {
            enemyDeck.DrawHand();
        }
    }
    public enum EncounterState
    {
        PlayerTurn,
        PlayerDamage,
        PlayerJokerExecution,
        ItemUse,
        PlayerSafe,
        PlayerEye,
        PlayerKnife,
        EnemyTurn,
        EnemyDamage,
        EnemySafe,
        EnemyJokerExecution,
        EncounterTutorial
    }
}
