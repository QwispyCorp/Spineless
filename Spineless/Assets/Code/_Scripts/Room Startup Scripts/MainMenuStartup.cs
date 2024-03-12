using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuStartup : MonoBehaviour
{
    [SerializeField] private GameObject TVCover;
    void Start()
    {
        AudioManager.Instance.PlayMusicTrack("Title Music");
        TVCover.SetActive(true);
    }
}
