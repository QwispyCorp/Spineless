using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

