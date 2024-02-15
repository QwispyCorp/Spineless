using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LightManager : MonoBehaviour
{
    public Light flickeringLight;
    public float timeDelay;
    private static LightManager _instance;
    private string _newSceneName;
    public bool hasStarted = false;
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

        if (!hasStarted)
        {
            SwitchLightOn();
            hasStarted = true;
            Debug.Log("LightSwitchOn");
        }
    }
    private void EnvironmentSwitchTo(string newScene)
    {
        if (newScene == "Encounter")
        {
            Debug.Log("Switching to Encounter Scene");
            SceneManager.LoadScene("Encounter");
        }
        else if (newScene == "GameBoard")
        {
            Debug.Log("Switching to GameBoard Scene");
            SceneManager.LoadScene("GameBoard");
        }
        else if (newScene == "ItemRoom")
        {
            Debug.Log("Switching to Item Room Scene");
            SceneManager.LoadScene("ItemRoom");
        }
        else if (newScene == "ChoiceRoom")
        {
            Debug.Log("Switching to Choice Room Scene");
            SceneManager.LoadScene("ChoiceRoom");
        }
        else if (newScene == "WinScene")
        {
            Debug.Log("Switching to Win Scene");
            SceneManager.LoadScene("WinScene");
        }
    }
    public void StartFlickeringTransitionTo(string newSceneName)
    {
        _newSceneName = newSceneName;
        StartCoroutine("FlickerTransition");
    }

    private IEnumerator FlickerTransition()
    {
        //Flicker Off 
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.1f, 0.4f);

        yield return new WaitForSeconds(2f);
        
        EnvironmentSwitchTo(_newSceneName);

        yield return new WaitForSeconds(2f);

        //Flicker On
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.1f, 0.4f);
        StopCoroutine("FlickerTransition");
    }
    public void DestroyLight()
    {
        Debug.Log("Destroy Light function called");

        Destroy(gameObject);
    }
    private IEnumerator SwitchLightOn()
    {
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.1f, 0.4f);
        StopCoroutine(SwitchLightOn());

    }
}
