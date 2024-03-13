using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Tutorial1Skipper : MonoBehaviour
{
    private bool isPressed;
    [SerializeField] private GameObject tutorialSubtitles;
    [SerializeField] private VideoPlayer video;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPressed)
        {
            isPressed = true;
            video.Stop();
            AudioManager.Instance.StopAllSounds();
            LightManager.Instance.StartFlickeringTransitionTo("GameBoard");
            AudioManager.Instance.UnMuffleMusic();
            tutorialSubtitles.SetActive(false);
        }
    }
}
