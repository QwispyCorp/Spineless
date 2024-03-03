using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTVAnimationTrigger : MonoBehaviour
{
    private void OnEnable()
    {
        LightManager.OnLightFlickeredOn += TurnOnTV;
    }
    private void OnDisable()
    {
        LightManager.OnLightFlickeredOn -= TurnOnTV;
    }

    void TurnOnTV()
    {
        GetComponent<Animator>().SetTrigger("TurnOn");
    }
}
