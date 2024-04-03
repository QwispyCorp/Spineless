using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSceneStartup : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    public Animator CameraAni;
    void Start()
    {
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
