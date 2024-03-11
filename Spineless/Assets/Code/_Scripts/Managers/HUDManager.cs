using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private GameObject[] overlays;
    //local and global manager instances
    private static HUDManager _instance;
    public static HUDManager Instance { get { return _instance; } } //to use any method from this manager call MenuManager.Instance."FunctionName"(); anywhere in any script

    void Awake()
    {
        //on awake check for existence of manager and handle accordingly
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void TurnOffHUD()
    {
        foreach (GameObject overlay in overlays)
        {
            overlay.SetActive(false);
        }
    }
}
