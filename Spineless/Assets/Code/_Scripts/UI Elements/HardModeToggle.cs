using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardModeToggle : MonoBehaviour
{
    [SerializeField] private GameDifficulty difficulty;
    [SerializeField] private PlayerSaveData saveData;
    public void ToggleHard()
    {
        difficulty.HardMode = !difficulty.HardMode;
        difficulty.NormalMode = !difficulty.NormalMode;

        if (difficulty.HardMode)
        {
            saveData.RemoveItems();
        }
        else if (difficulty.NormalMode)
        {
            saveData.AddCamera();
        }
    }
}
