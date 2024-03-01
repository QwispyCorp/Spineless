using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void ToMainMenu()
    {
        if (BoardGenerator.Instance != null)
        {
            BoardGenerator.Instance.DestroyBoard();
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopMusicTrack(AudioManager.Instance.CurrentTrack);
        }
        SceneManager.LoadScene("MainMenu");
    }
}
