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
    private static PlayerHealthTest _instance;
    public static PlayerHealthTest Instance { get { return _instance; } } //to use any method from this manager call MenuManager.Instance."FunctionName"(); anywhere in any script

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
        healthText.SetText("Fingers: " + playerHealth.Value);
    }
    public void ChangeHealth(int amount)
    {
        playerHealth.Value += amount;
        healthText.SetText("Fingers: " + playerHealth.Value);
        if (playerHealth.Value <= 0)
        {
            HUDManager.Instance.TurnOffHUD();
            PlayerLoses();
        }
    }

    public int GetCurrentHealth()
    {
        return playerHealth.Value;
    }

    public int GetMaxHealth()
    {
        return playerMaxHealth.Value;
    }
    private void PlayerLoses()
    {
        //We can add more stuff here later such as death animations
        saveData.playerFingersInNextEncounter -= 2; 
        LightManager.Instance.StartFlickeringTransitionTo("GameBoard");
    }
}
