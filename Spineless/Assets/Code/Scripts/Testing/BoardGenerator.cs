using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private Canvas gameBoard;
    [SerializeField] private GameObject[] tilePrefabs;
    private bool boardGenerated = false;
    private int boardSize = 8;
    private float tileSpacing = 0.04f;

    void Start()
    {
        if (!boardGenerated)
        {
            GenerateBoard();
        }
    }

    private void GenerateBoard()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Instantiate(tilePrefabs[UnityEngine.Random.Range(0, tilePrefabs.Length)], transform.position + new Vector3(i * tileSpacing, 0, j * tileSpacing), Quaternion.Euler(90, 0, 0), gameBoard.transform);
            }
        }
    }
}
