using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Game Difficulty", menuName = "Game/Game Difficulty", order = 0)]

public class GameDifficulty : ScriptableObject
{
    public bool HardMode;
    public bool NormalMode;

    private void OnDisable()
    { // make game difficulty normal by default
        HardMode = false;
        NormalMode = true;
    }
}
