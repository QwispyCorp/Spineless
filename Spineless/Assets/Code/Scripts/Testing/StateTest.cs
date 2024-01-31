using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateTest : MonoBehaviour
{
    private static StateTest _instance;
    public static StateTest Instance { get { return _instance; } }
    public EncounterState CurrentEncounterState;
    [SerializeField] private EnemyDeckLogic enemyDeck;
    [SerializeField] private PlayerDeckLogic playerDeck;
    [SerializeField] private float delayTime;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
        UpdateEncounterState(EncounterState.PlayerTurn);
    }
    public void UpdateEncounterState(EncounterState newState)
    {
        CurrentEncounterState = newState;

        switch (newState)
        {
            case EncounterState.PlayerTurn:
                HandlePlayerTurn();
                //StartCoroutine("TurnDelay");
                break;
            case EncounterState.PlayerDamage:
                HandlePlayerDamage();
                //StartCoroutine("TurnDelay");
                break;
            case EncounterState.PlayerSafe:
                HandlePlayerSafe();
                //StartCoroutine("TurnDelay");
                break;
            case EncounterState.EnemyTurn:
                HandleEnemyTurn();
                //StartCoroutine("TurnDelay");
                break;
            case EncounterState.EnemyDamage:
                HandleEnemyDamage();
                //StartCoroutine("TurnDelay");
                break;
            case EncounterState.EnemySafe:
                HandleEnemySafe();

                break;
        }
    }

    public void DelayTurn()
    {
        StartCoroutine("TurnDelay");
    }

    private void HandlePlayerTurn()
    {
        Debug.Log("PLAYER CURRENTLY IN: Player Turn State");
    }
    private void HandlePlayerDamage()
    {
        //damage animation and health change logic goes here
        //HealthTest.Instance.ChangeHealth(-1);
        Debug.Log("PLAYER CURRENTLY IN: Player Damage State");
        Debug.Log("Switching to Enemy turn...");
        StartCoroutine("TurnDelay");
        UpdateEncounterState(EncounterState.EnemyTurn);
    }
    private void HandlePlayerSafe()
    {
        Debug.Log("PLAYER CURRENTLY IN: Player Safe State.");
        Debug.Log("Switching to Enemy turn...");
        StartCoroutine("TurnDelay");
        UpdateEncounterState(EncounterState.EnemyTurn);
    }
    private void HandleEnemyTurn()
    {
        Debug.Log("ENEMY CURRENTLY IN: Enemy Turn State");
        //Enemy AI Logic for picking card here
        enemyDeck.EnemyCardSelection();
        Debug.Log("Switching to Player Turn");
        StartCoroutine("TurnDelay");
        UpdateEncounterState(EncounterState.PlayerTurn);
    }
    private void HandleEnemyDamage()
    {
        //Enemy damage animation and health change logic goes here
        Debug.Log("ENEMY CURRENTLY IN: Enemy Damage State");
        //Enemy Damage Logic/ Animations
        Debug.Log("Switching to player turn...");
        StartCoroutine("TurnDelay");
        UpdateEncounterState(EncounterState.PlayerTurn);
    }
    private void HandleEnemySafe()
    {
        Debug.Log("ENEMY CURRENTLY IN: Enemy Safe State");
        //Enemy Safe Logic/ Animations
        Debug.Log("Switching to player turn...");
        StartCoroutine("TurnDelay");
        UpdateEncounterState(EncounterState.PlayerTurn);

    }

    public enum EncounterState
    {
        PlayerTurn,
        PlayerDamage,
        PlayerSafe,
        EnemyTurn,
        EnemyDamage,
        EnemySafe,
    }
    private IEnumerator TurnDelay()
    {
        Debug.Log("Delaying turn");
        yield return new WaitForSeconds(delayTime);
    }
}
