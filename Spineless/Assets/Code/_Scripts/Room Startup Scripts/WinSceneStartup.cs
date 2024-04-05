using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSceneStartup : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private GameDifficulty difficulty;
    public Animator CameraAni;
    void Start()
    {
        if (difficulty.HardMode)
        {
            SteamIntegration.UnlockAchievement("HardWin");
        }
        else if (difficulty.NormalMode)
        {
            SteamIntegration.UnlockAchievement("NormalWin");
        }
        
        Invoke("ShowWinScreen", 8);
        CameraAni.SetTrigger("Win");

        if (BoardGenerator.Instance != null)
        {
            BoardGenerator.Instance.DestroyBoard(); //destroy the current board
        }
        if (AudioManager.Instance != null) //
        {
            AudioManager.Instance.StopMusicTrack(AudioManager.Instance.CurrentTrack);
            AudioManager.Instance.StopAllSounds();
        }

        saveData.ClearAllData();
    }

    private void ShowWinScreen()
    {
        saveData.ClearAllData();
        LightManager.Instance.StartFlickeringTransitionTo("MainMenu");
    }
}
