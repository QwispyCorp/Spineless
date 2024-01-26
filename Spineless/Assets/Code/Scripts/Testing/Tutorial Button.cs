using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialButton : MonoBehaviour
{
    public Canvas tutorialCanvas;

    void Start()
    {
        tutorialCanvas.gameObject.SetActive(true);
    }

    public void SwitchOffCanvas()
    {
        tutorialCanvas.gameObject.SetActive(false);
    }

    public void SwitchToEncounter()
    {
        GameObject lightGameObject = GameObject.FindGameObjectWithTag("Light");
        if (lightGameObject != null)
        {
            LightManager lightManager = lightGameObject.GetComponent<LightManager>();

            if (lightManager != null)
            {
                CoroutineManager.Instance.StartCoroutine(lightManager.StartFlickeringTransition());
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

