using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;

    //Tile Objects assigned in inspector
    [SerializeField] private GameObject encounterTile;
    [SerializeField] private GameObject itemTile;
    [SerializeField] private GameObject emptyTile;
    [SerializeField] private GameObject winTile;
    //Max values of tiles on the board assigned in inspector
    [SerializeField] private int maxEncounterTiles;
    [SerializeField] private int maxItemTiles;
    //private utility variables
    public bool boardGenerated = false;
    private int boardSize = 8;
    private float tileSpacing = 0.04f;
    private List<GameObject> generatedBoardTiles;
    //Singleton variables
    private static BoardGenerator _instance;
    public static BoardGenerator Instance { get { return _instance; } }


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        generatedBoardTiles = new List<GameObject>();
        gameObject.SetActive(true);
        if (!boardGenerated) //if board hasnt been generated, generate it
        {
            InitializeTiles();
            GenerateBoard();
        }
    }
    private void InitializeTiles()
    {
        int encounterTiles = 0;
        int emptyTiles = 0;
        int itemTiles = 0;
        int winTiles = 0;
        for (int i = 0; i < boardSize * boardSize; i++)
        {
            if (winTiles < 1)
            {
                generatedBoardTiles.Add(winTile); //add win tile to list
                winTiles++;
                Debug.Log("Generated Win Tile # " + winTiles);
            }
            if (itemTiles < maxItemTiles)
            {
                generatedBoardTiles.Add(itemTile); //add item tiles to list
                itemTiles++;
                Debug.Log("Generated Item Tile # " + itemTiles);
            }
            else if (encounterTiles < maxEncounterTiles)
            {
                generatedBoardTiles.Add(encounterTile); //add encounter tiles to list
                encounterTiles++;
                Debug.Log("Generated Encounter Tile # " + encounterTiles);
            }
            else if (encounterTiles == maxEncounterTiles && itemTiles == maxItemTiles)
            {
                generatedBoardTiles.Add(emptyTile); //add empty tiles to list
                emptyTiles++;
                Debug.Log("Generated Empty Tile # " + emptyTiles);
            }
        }
    }

    private void GenerateBoard()
    {
        int winTileSlot = UnityEngine.Random.Range(0, boardSize - 1); //random column number for spawning the winning tile on the board

        Debug.Log("Win Tile Spawned at (" + winTileSlot + "," + (boardSize - 1) + ")");
        //Spawn the winning tile
        GameObject winningTile = generatedBoardTiles.Find(tile => tile.name == "Win Tile");
        Instantiate(winTile, transform.position + new Vector3(winTileSlot * tileSpacing, 0, (boardSize - 1) * tileSpacing), Quaternion.identity, transform);
        generatedBoardTiles.Remove(winningTile);

        for (int i = 0; i < boardSize; i++) //column number of board
        {
            for (int j = 0; j < boardSize; j++) //row number of board
            {

                if (i == 3 && j == 0) //always spawn empty tile at player spawnpoint
                {
                    GameObject randomEmptyTile = generatedBoardTiles.Find(tile => tile.name == "Empty Tile");
                    Instantiate(emptyTile, transform.position + new Vector3(i * tileSpacing, 0, j * tileSpacing), Quaternion.identity, transform);
                    generatedBoardTiles.Remove(randomEmptyTile);
                }
                else if (i == winTileSlot && j == boardSize - 1) //skip random instantiation on winning tile
                {
                    continue;
                }
                else
                {
                    int randomTileIndex = UnityEngine.Random.Range(0, generatedBoardTiles.Count);

                    GameObject randomTile = generatedBoardTiles[randomTileIndex];
                    GameObject randomTileInstance = Instantiate(randomTile, transform.position + new Vector3(i * tileSpacing, 0, j * tileSpacing), Quaternion.identity, transform);
                    generatedBoardTiles.Remove(randomTile);
                }
            }
        }
        boardGenerated = true;
    }
    public void DestroyBoard()
    {
        Destroy(gameObject);
    }
    public void HideBoard()
    {
        gameObject.SetActive(false);
    }
    public void SpawnBoard()
    {
        gameObject.SetActive(true);
    }
}
