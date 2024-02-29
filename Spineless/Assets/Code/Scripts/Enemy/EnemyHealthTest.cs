using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyHealthTest : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private int maxHealth;
    [SerializeField] private IntegerReference enemyHealth;
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private EncounterData encounterData;
    public delegate void EnemyFingerLost();
    public static event EnemyFingerLost OnEnemyFingerLost;
    private static EnemyHealthTest _instance;
    public static EnemyHealthTest Instance { get { return _instance; } } //to use any method from this manager call MenuManager.Instance."FunctionName"(); anywhere in any script

    void OnEnable()
    {
        EnemyAnimationTrigger.OnEnemyAnimationFinished += PlayerWinsEncounter;
    }
    void OnDisable()
    {
        EnemyAnimationTrigger.OnEnemyAnimationFinished -= PlayerWinsEncounter;
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
        enemyHealth.Value = maxHealth;
        healthText.SetText("Enemy Fingers: " + enemyHealth.Value);
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (OnEnemyFingerLost != null)
            {
                OnEnemyFingerLost?.Invoke();
            }
        }
        enemyHealth.Value += amount;
        healthText.SetText("Enemy Fingers: " + enemyHealth.Value);
    }
    public int GetCurrentHealth()
    {
        return enemyHealth.Value;
    }
    private void PlayerWinsEncounter()
    {
        if (enemyHealth.Value <= 0)
        {
            HUDManager.Instance.TurnOffHUD();
            PlayerWinsEncounter();
            AudioManager.Instance.PlaySound("Riser");
            saveData.EncountersWon++;
            GameObject lightGameObject = GameObject.FindGameObjectWithTag("Light");
            if (lightGameObject != null)
            {
                LightManager lightManager = lightGameObject.GetComponent<LightManager>();

                if (lightManager != null)
                {
                    encounterData.ClearAllData();
                    LightManager.Instance.StartFlickeringTransitionTo("GameBoard");
                }
                else
                {
                    Debug.LogError("LightManager component not found on GameObject with tag 'Light'.");
                }
            }
            else
            {
                Debug.LogError("GameObject with tag 'Light' not found in the scene.");
            }
        }
    }
}
