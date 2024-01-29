using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class EnemyHealthTest : MonoBehaviour
{
    [SerializeField]
    private TMP_Text healthText;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private IntegerReference playerHealth;
    private static EnemyHealthTest _instance;
    public static EnemyHealthTest Instance { get { return _instance; } } //to use any method from this manager call MenuManager.Instance."FunctionName"(); anywhere in any script

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
        playerHealth.Value = maxHealth;
        healthText.SetText("Enemy Fingers: " + playerHealth.Value);
    }
    public void ChangeHealth(int amount){
        playerHealth.Value += amount;
        healthText.SetText("Enemy Fingers: " + playerHealth.Value);
        if (playerHealth.Value <= 0)
        {
            PlayerWins();
        }
    }
    private void PlayerWins()
    {  
            GameObject lightGameObject = GameObject.FindGameObjectWithTag("Light");
            if (lightGameObject != null)
            {
                LightManager lightManager = lightGameObject.GetComponent<LightManager>();

                if (lightManager != null)
                {
                    LightManager.Instance.StartCoroutine(lightManager.StartFlickeringTransition());
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
