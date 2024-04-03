using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardModeToggle : MonoBehaviour
{
    [SerializeField] private GameDifficulty difficulty;
    public void ToggleHard()
    {
        difficulty.HardMode = !difficulty.HardMode;
        difficulty.NormalMode = !difficulty.NormalMode;
    }
}
