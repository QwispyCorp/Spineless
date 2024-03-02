using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuStartup : MonoBehaviour
{
    
    void Start()
    {
        AudioManager.Instance.PlayMusicTrack("Title Music");
    }
}
