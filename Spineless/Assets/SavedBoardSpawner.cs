using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedBoardSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (BoardGenerator.Instance.boardGenerated == true)
        {
            BoardGenerator.Instance.SpawnBoard();
        }
    }
}
