using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LightManager : MonoBehaviour
{
    public Light flickeringLight;
    public Animator Light;
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

        StopCoroutine(FlickeringTransition());
    }

    private void EnviromentSwitch()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Prototype")
        {
            SceneManager.LoadScene("GameBoard"); 
        }
        else
        {
            SceneManager.LoadScene("Prototype");
        }
    }
}
