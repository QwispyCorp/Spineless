using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HardModeToggle : MonoBehaviour
{
    [SerializeField] private GameDifficulty difficulty;
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private Toggle toggle;
    void OnEnable()
    {
        if (difficulty.HardMode)
        {
            toggle.SetIsOnWithoutNotify(true);
        }

    }
    public void ToggleHard()
    {
        Debug.Log("Toggling difficulty");
        if (gameObject.activeSelf)
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
}
