using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject OptionsMenu;
    public GameObject TransitionMenu;

    private bool isPaused = false;
    private string currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        OptionsMenu.SetActive(false);
        ResumeGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public bool IsPaused()
    {
        return isPaused;
    }
    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        //Time.timeScale = 1;
        isPaused = false;
        pauseMenu.SetActive(false);
        if (currentScene == "Encounter")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
    public void LoadMenu()
    {
        if (BoardGenerator.Instance != null)
        {
            BoardGenerator.Instance.DestroyBoard();
        }
        SceneManager.LoadScene("MainMenu");
    }
    //-----------------------------------------------------
    public void LoadOptions() //call this function on a button to switch to options
    {
        pauseMenu.SetActive(false);
        TransitionMenu.SetActive(true);
        Invoke("TransitiontoOptions", 1f);
    }
    public void TransitiontoOptions()
    {
        TransitionMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }
    //----------------------------------------------------
    public void LoadPause() //call this function on the button
    {
        OptionsMenu.SetActive(false);
        TransitionMenu.SetActive(true);
        Invoke("TransitiontoPause", 1f);
    }
    public void TransitiontoPause()
    {
        TransitionMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
