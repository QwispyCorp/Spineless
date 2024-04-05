using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Game Difficulty", menuName = "Game/Game Difficulty", order = 0)]

public class GameDifficulty : ScriptableObject
{
    [SerializeField] private PlayerSaveData saveData;
    public bool HardMode;
    public bool NormalMode;
    public bool gameStarted;

    private void OnEnable()
    { // make game difficulty normal by default
        Debug.Log("Game difficulty resetting OnEnable");
        HardMode = false;
        NormalMode = true;
    }
    public void ToggleHard()
    {
        Debug.Log("Toggling difficulty");
        HardMode = !HardMode;
        NormalMode = !NormalMode;

        if (HardMode)
        {
            saveData.RemoveItems();
        }
        else if (NormalMode)
        {
            saveData.AddCamera();
        }

    }
}
