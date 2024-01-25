using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Light flickeringLight;
    public Animator Light;

    void Start()
    {
        StartCoroutine(FlickeringTransition());
    }

    IEnumerator FlickeringTransition()
    {
        // Flickering Animation
        Light.SetTrigger("Off");

        yield return new WaitForSeconds(2f);

        // Triggering Function
        EnviromentSwitch();

        yield return new WaitForSeconds(2f);

        // Flickering Back On Animation
        Light.SetTrigger("On");
    }

    void EnviromentSwitch()
    {
        // Implement Enviroment Switch
    }
}
