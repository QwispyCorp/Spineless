using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpawner : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    void Start()
    {
        if(BoardGenerator.Instance.boardGenerated == true){
            BoardGenerator.Instance.SpawnBoard();
        }
    }
}
