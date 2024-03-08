using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LightManager : MonoBehaviour
{
    public float timeDelay;
    private static LightManager _instance;
    private string _newSceneName;
    public GameObject Lights;
    public static LightManager Instance { get { return _instance; } }

    public delegate void LightFlickeredOn();
    public static event LightFlickeredOn OnLightFlickeredOn;

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
    }
    private void Start()
    {
        StartCoroutine("SwitchLightOn");
    }
    private void EnvironmentSwitchTo(string newScene)
    {
        AudioManager.Instance.StopMusicTrack(AudioManager.Instance.CurrentTrack);

        if (newScene == "Encounter")
        {
            Debug.Log("Switching to Encounter Scene");
            SceneManager.LoadScene("Encounter");
            //AudioManager.Instance.PlayMusicTrack("Encounter Music");
        }
        else if (newScene == "GameBoard")
        {
            Debug.Log("Switching to GameBoard Scene");
            SceneManager.LoadScene("GameBoard");
            AudioManager.Instance.StopAllSounds();
        }
        else if (newScene == "ItemRoom")
        {
            Debug.Log("Switching to Item Room Scene");
            SceneManager.LoadScene("ItemRoom");
            AudioManager.Instance.PlayMusicTrack("Item Room Music");
        }
        else if (newScene == "ShopRoom")
        {
            Debug.Log("Switching to Shop Room Scene");
            SceneManager.LoadScene("ShopRoom");
            AudioManager.Instance.PlayMusicTrack("Shop Room Music");
        }
        else if (newScene == "WinScene")
        {
            Debug.Log("Switching to Win Scene");
            SceneManager.LoadScene("WinScene");
        }
        else if (newScene == "EncounterTutorial")
        {
            Debug.Log("Switching to Encounter Tutorial");
            SceneManager.LoadScene("EncounterTutorial");
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
        AudioManager.Instance.PlaySound("LightSwitch1"); //light off
        Lights.gameObject.SetActive(false);
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        AudioManager.Instance.PlaySound("LightSwitch2"); //light on
        Lights.gameObject.SetActive(true);
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        AudioManager.Instance.PlaySound("LightSwitch1"); //light off
        Lights.gameObject.SetActive(false);
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        AudioManager.Instance.PlaySound("LightSwitch2"); //light on
        Lights.gameObject.SetActive(true);
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        AudioManager.Instance.PlaySound("LightSwitch1"); //light off
        Lights.gameObject.SetActive(false);
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        AudioManager.Instance.PlaySound("LightSwitch2"); //light on
        Lights.gameObject.SetActive(true);
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        AudioManager.Instance.PlaySound("LightSwitch1"); //light off
        Lights.gameObject.SetActive(false);
        timeDelay = Random.Range(0.1f, 0.4f);

        yield return new WaitForSeconds(2f);

        EnvironmentSwitchTo(_newSceneName);
        StopCoroutine("FlickerTransition");
    }
    private IEnumerator SwitchLightOn()
    {

        AudioManager.Instance.PlaySound("LightSwitch" + UnityEngine.Random.Range(1, 6)); //light on
        Lights.gameObject.SetActive(true);
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        AudioManager.Instance.PlaySound("LightSwitch" + UnityEngine.Random.Range(1, 6)); //light off
        Lights.gameObject.SetActive(false);
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        AudioManager.Instance.PlaySound("LightSwitch" + UnityEngine.Random.Range(1, 6)); //light on
        Lights.gameObject.SetActive(true);
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        AudioManager.Instance.PlaySound("LightSwitch" + UnityEngine.Random.Range(1, 6)); //light off
        Lights.gameObject.SetActive(false);
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        AudioManager.Instance.PlaySound("LightSwitch" + UnityEngine.Random.Range(1, 6)); //light on
        Lights.gameObject.SetActive(true);
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        AudioManager.Instance.PlaySound("LightSwitch" + UnityEngine.Random.Range(1, 6)); ; //light off
        Lights.gameObject.SetActive(false);
        timeDelay = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        AudioManager.Instance.PlaySound("LightSwitch" + UnityEngine.Random.Range(1, 6)); //light on
        Lights.gameObject.SetActive(true);
        timeDelay = Random.Range(0.1f, 0.4f);
        if (OnLightFlickeredOn != null)
        {
            OnLightFlickeredOn?.Invoke();
        }
        StopCoroutine(SwitchLightOn());

    }
}
