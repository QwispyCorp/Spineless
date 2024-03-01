using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1Skipper : MonoBehaviour
{
    private bool isPressed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPressed)
        {
            isPressed = true;
            AudioManager.Instance.StopAllSounds();
            LightManager.Instance.StartFlickeringTransitionTo("GameBoard");
            AudioManager.Instance.UnMuffleMusic();
        }
    }
}
