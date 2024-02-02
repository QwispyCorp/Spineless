using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void ToMainMenu()
    {
        BoardGenerator.Instance.DestroyBoard();
        SceneManager.LoadScene("MainMenu");
    }
}
