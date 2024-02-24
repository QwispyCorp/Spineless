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
        SceneManager.LoadScene("MainMenu");
    }
}
