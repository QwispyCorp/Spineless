using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealthTest : MonoBehaviour
{
    [SerializeField]
    private TMP_Text healthText;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private IntegerReference playerHealth;
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
        playerHealth.Value = maxHealth;
        healthText.SetText("Fingers: " + playerHealth.Value);
        if (playerHealth.Value <= 0)
        {
            PlayerLoses();
        }
    }
    public void ChangeHealth(int amount) {
        playerHealth.Value += amount;
        healthText.SetText("Fingers: " + playerHealth.Value);
    }

    private void PlayerLoses()
    {
        //We can add more stuff here later such as death animations
        SceneManager.LoadScene("PlayerLost");
    }
}
