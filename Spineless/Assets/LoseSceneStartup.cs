using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseSceneStartup : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SwitchToMain", 3);
    }

    private void SwitchToMain()
    {
        saveData.ClearAllData();
        LightManager.Instance.StartFlickeringTransitionTo("MainMenu");

    }
}
