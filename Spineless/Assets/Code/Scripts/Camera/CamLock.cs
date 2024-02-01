using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamLock : MonoBehaviour
{
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Prototype")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (currentScene.name == "GameBoard")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
