using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void ToMainMenu()
    {
        BoardGenerator.Instance.boardGenerated = false; //reset the game board when starting game over
        SceneManager.LoadScene("MainMenu");
    }
}
