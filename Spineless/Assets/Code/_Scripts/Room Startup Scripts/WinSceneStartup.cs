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
    }

    private void ShowWinScreen()
    {
        saveData.ClearAllData();
        LightManager.Instance.StartFlickeringTransitionTo("MainMenu");
    }
}
