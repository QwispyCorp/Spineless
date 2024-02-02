using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuStartup : MonoBehaviour
{
    
    void Start()
    {
        MenuManager.Instance.OpenMenu("Main Menu");
        AudioManager.Instance.PlayMusicTrack("Title Music");
    }
}
