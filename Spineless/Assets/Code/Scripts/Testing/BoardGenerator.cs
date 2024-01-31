using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    //Tile Objects assigned in inspector
    [SerializeField] private GameObject encounterTile;
    [SerializeField] private GameObject itemTile;
    [SerializeField] private GameObject emptyTile;
    //Max values of tiles on the board assigned in inspector
    [SerializeField] private int maxEncounterTiles;
    [SerializeField] private int maxItemTiles;
    //private utility variables
    private bool boardGenerated = false;
    private int boardSize = 8;
    private float tileSpacing = 0.04f;
    private List<GameObject> boardTiles;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        boardTiles = new List<GameObject>();

        if (!boardGenerated)
        {
            InitializeTiles();
            GenerateBoard();
        }
    }

    void Start()
    {
        gameObject.SetActive(true);
    }
    private void InitializeTiles()
    {
        int encounterTiles = 0;
        int itemTiles = 0;
        for (int i = 0; i < boardSize * boardSize; i++)
        {
            if (itemTiles < maxItemTiles)
            {
                boardTiles.Add(itemTile); //add item tiles to list
                itemTiles++;
            }
            else if (encounterTiles < maxEncounterTiles)
            {
                boardTiles.Add(encounterTile); //add encounter tiles to list
                encounterTiles++;
            }
            else if (encounterTiles == maxEncounterTiles && itemTiles == maxItemTiles)
            {
                boardTiles.Add(emptyTile); //add empty tiles to list
            }
        }
        Debug.Log(boardTiles.Count);
    }

    private void GenerateBoard()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                int randomTileIndex = UnityEngine.Random.Range(0, boardTiles.Count);
                GameObject randomTile = boardTiles[randomTileIndex];
                Instantiate(randomTile, transform.position + new Vector3(i * tileSpacing, 0, j * tileSpacing), Quaternion.identity, transform);
                boardTiles.Remove(randomTile);
            }
        }
    }
}
