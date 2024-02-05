using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LightManager : MonoBehaviour
{
    public Light flickeringLight;
    public Animator Light;
    private static LightManager _instance;
    public static LightManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
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
            Debug.Log("Switching to GameBoard Scene");
            SceneManager.LoadScene("GameBoard");
        }
        else if (currentScene.name == "GameBoard")
        {
            Debug.Log("Switching to Prototype Scene");
            SceneManager.LoadScene("Prototype");
        }
    }
    public void DestroyLight()
    {
        Debug.Log("Destroy Light function called");

        Destroy(gameObject);
    }
}
