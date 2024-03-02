using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealthTest : MonoBehaviour
{
    [SerializeField]
    private TMP_Text healthText;
    private int maxHealth;
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private IntegerReference playerHealth;
    [SerializeField] private IntegerReference playerMaxHealth;
    [SerializeField] private EncounterData encounterData;
    public delegate void PlayerFingerLost();
    public static event PlayerFingerLost OnPlayerFingerLost;
    private static PlayerHealthTest _instance;
    public static PlayerHealthTest Instance { get { return _instance; } } //to use any method from this manager call MenuManager.Instance."FunctionName"(); anywhere in any script

    void OnEnable()
    {
        PlayerAnimationTrigger.OnAnimationFinished += PlayerLosesEncounter;
    }
    void OnDisable()
    {
        PlayerAnimationTrigger.OnAnimationFinished -= PlayerLosesEncounter;
    }

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
        playerHealth.Value = saveData.playerFingersInNextEncounter;
        //healthText.SetText("Fingers: " + playerHealth.Value);
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0) //if player health change is negative
        {
            if (OnPlayerFingerLost != null)
            {
                OnPlayerFingerLost?.Invoke(); //invoke finger lost event
            }
        }
        playerHealth.Value += amount;
        //healthText.SetText("Fingers: " + playerHealth.Value);
    }

    public int GetCurrentHealth()
    {
        return playerHealth.Value;
    }

    public int GetMaxHealth()
    {
        return playerMaxHealth.Value;
    }
    private void PlayerLosesEncounter()
    {
        if (playerHealth.Value <= 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            //HUDManager.Instance.TurnOffHUD();
            saveData.playerFingersInNextEncounter -= 2; 
            if (saveData.playerFingersInNextEncounter <= 0) //if player's perma health drops below 0
            {
                //player perma loses
                PopUpTextManager.Instance.ShowScreen("Lose Screen"); //show lose screen or lose animation
            }
            else //if player's still alive after enocunter
            {
                encounterData.ClearAllData(); //reset encounter data
                LightManager.Instance.StartFlickeringTransitionTo("GameBoard"); //transition to game board
            }
        }
    }
}
