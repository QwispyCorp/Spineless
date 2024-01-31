using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LightManager : MonoBehaviour
{
    public Light flickeringLight;
    public Animator Light;
    //private string LightOn = "On";
   // private string LightOff = "Off";
    //private static LightManager instance;
    public static LightManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public IEnumerator StartFlickeringTransition()
    {
        return FlickeringTransition();
    }

    private IEnumerator FlickeringTransition()
    {
        Light.SetTrigger("TurnOff");

        yield return new WaitForSeconds(4f);

        EnviromentSwitch();

        yield return new WaitForSeconds(2f);

        if (Light != null)
        {
            Light.SetTrigger("TurnOn");
        }
        else
        {
            Debug.LogError("Animator component not found on LightManager.");
        }

        yield return new WaitForSeconds(4f);
    }

    private void EnviromentSwitch()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Prototype")
        {
            if (PlayerHealthTest.Instance.GetCurrentHealth() <= 0)
            {
                PopUpTextManager.Instance.ShowScreen("Lose Screen");
            }
            else
            {
                SceneManager.LoadScene("GameBoard");
            }

        }
        else if (currentScene.name == "GameBoard")
        {
            SceneManager.LoadScene("Prototype");
        }
    }
}
