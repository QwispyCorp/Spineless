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
        if (LightManager.Instance != null)
        {
            LightManager.Instance.DestroyLight();
        }
        SceneManager.LoadScene("MainMenu");
    }
}
