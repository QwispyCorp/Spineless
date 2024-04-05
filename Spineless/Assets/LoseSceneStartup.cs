using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseSceneStartup : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    // Start is called before the first frame update
    void Start()
    {
        SteamIntegration.UnlockAchievement("LoseGame");
        Invoke("SwitchToMain", 3);

        if (BoardGenerator.Instance != null)
        {
            BoardGenerator.Instance.DestroyBoard(); //destroy the current board
        }
        if (AudioManager.Instance != null) //
        {
            AudioManager.Instance.StopMusicTrack(AudioManager.Instance.CurrentTrack);
            AudioManager.Instance.StopAllSounds();
        }
    }

    private void SwitchToMain()
    {
        saveData.ClearAllData();
        LightManager.Instance.StartFlickeringTransitionTo("MainMenu");

    }
}
