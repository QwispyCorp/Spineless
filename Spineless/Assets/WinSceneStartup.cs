using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSceneStartup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShowWinScreen", 3);
    }

    private void ShowWinScreen()
    {
        PopUpTextManager.Instance.ShowScreen("Win Screen");
    }
}
