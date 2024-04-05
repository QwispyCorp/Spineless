using System.Collections;
using System.Collections.Generic;
using Steamworks.Data;
using UnityEngine;

public class MainMenuStartup : MonoBehaviour
{
    [SerializeField] private GameObject TVCover;
    [SerializeField] private GameDifficulty difficulty;
    void Start()
    {
        AudioManager.Instance.PlayMusicTrack("Title Music");
        TVCover.SetActive(true);
        Debug.Log("Hard Difficulty: " + difficulty.HardMode);
        Debug.Log("Normal Difficulty: " + difficulty.NormalMode);
    }
}
