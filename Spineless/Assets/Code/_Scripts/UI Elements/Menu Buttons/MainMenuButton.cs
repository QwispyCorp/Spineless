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
            if (AudioManager.Instance.CurrentTrack != null)
            {
                AudioManager.Instance.StopAllSounds();
                AudioManager.Instance.StopMusicTrack(AudioManager.Instance.CurrentTrack);
            }
        }
        SceneManager.LoadScene("MainMenu");
    }
}
