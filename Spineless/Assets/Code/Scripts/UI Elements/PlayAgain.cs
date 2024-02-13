using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public void PlayAgainFunc()
    {
        if (BoardGenerator.Instance != null)
        {
            BoardGenerator.Instance.DestroyBoard();
        }
        GameObject lightGameObject = GameObject.FindGameObjectWithTag("Light");
        if (lightGameObject != null)
        {
            LightManager lightManager = lightGameObject.GetComponent<LightManager>();

            if (lightManager != null)
            {
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
