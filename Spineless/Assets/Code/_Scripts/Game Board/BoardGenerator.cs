using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BoardGenerator : MonoBehaviour
{
    [Header("Game Data")]
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private GameDifficulty difficulty;
    [Header("Tile Prefabs")]

    //Tile Objects assigned in inspector
    [SerializeField] private GameObject encounterTile;
    [SerializeField] private GameObject itemTile;
    [SerializeField] private GameObject shopTile;
    [SerializeField] private GameObject emptyTile;
    [SerializeField] private GameObject winTile;
    [Header("Board Properties")]
    //Max values of tiles on the board assigned in inspector
    [SerializeField] private int maxEncounterTiles;
    [SerializeField] private int maxItemTiles;
    [SerializeField] private int maxShopTiles;
    [HideInInspector] public bool boardGenerated = false;

    [Header("Tile Properties")]
    [SerializeField] private float tileScaleFactor;
    //private utility variables
    private int boardSize = 8;
    private float tileSpacing;
    private List<GameObject> generatedBoardTiles;
    private List<GameObject> tileList;
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
        tileSpacing = .04f * tileScaleFactor;
        saveData.TileSpacing = tileSpacing;
        DontDestroyOnLoad(gameObject);

        generatedBoardTiles = new List<GameObject>();
        tileList = new List<GameObject>();
        gameObject.SetActive(true);
        if (!boardGenerated) //if board hasnt been generated, generate it
        {
            //set amounts of tiles here
            if (difficulty.HardMode)
            {
                maxEncounterTiles = 12;
                maxItemTiles = 0;
                maxShopTiles = 1;
            }
            else
            {
                maxEncounterTiles = 12;
                maxShopTiles = 1;
                maxItemTiles = 15;
            }
            InitializeTiles();
            GenerateBoard();
        }
    }

    void Start()
    {
        boardGenerated = true;
    }
    private void InitializeTiles()
    {
        int encounterTiles = 0;
        int emptyTiles = 0;
        int itemTiles = 0;
        int winTiles = 0;
        int shopTiles = 0;
        for (int i = 0; i < boardSize * boardSize; i++)
        {
            if (winTiles < 1)
            {
                generatedBoardTiles.Add(winTile); //add win tile to list
                winTiles++;
            }
            if (itemTiles < maxItemTiles)
            {
                generatedBoardTiles.Add(itemTile); //add item tiles to list
                itemTiles++;
            }
            else if (encounterTiles < maxEncounterTiles)
            {
                generatedBoardTiles.Add(encounterTile); //add encounter tiles to list
                encounterTiles++;
            }
            else if (shopTiles < maxShopTiles)
            {
                generatedBoardTiles.Add(shopTile); //add shop tile to list
                shopTiles++;
            }
            else
            {
                generatedBoardTiles.Add(emptyTile); //add empty tiles to list
                emptyTiles++;
            }
        }
    }

    private void GenerateBoard()
    {
        int currentTile = 0;

        GameObject randomShopTile = new GameObject();
        GameObject shopInstance = new GameObject();
        int winTileSlot = UnityEngine.Random.Range(0, boardSize - 1); //random column number for spawning the winning tile on the board

        Debug.Log("Win Tile Spawned at (" + winTileSlot + "," + (boardSize - 1) + ")");
        //Spawn the winning tile
        GameObject winningTile = generatedBoardTiles.Find(tile => tile.name == "Win Tile");
        winningTile.transform.localScale = new Vector3(tileScaleFactor, tileScaleFactor, tileScaleFactor);
        Instantiate(winTile, transform.position + new Vector3(winTileSlot * tileSpacing, 0, (boardSize - 1) * tileSpacing), Quaternion.identity, transform);
        generatedBoardTiles.Remove(winningTile);


        //Always spawn shop tile in center of board
        randomShopTile = generatedBoardTiles.Find(tile => tile.name == "Shop Tile"); //reference to shop tile
        randomShopTile.transform.localScale = new Vector3(tileScaleFactor, tileScaleFactor, tileScaleFactor);//resize tile to fit board
        shopInstance = Instantiate(shopTile, transform.position + new Vector3(4 * tileSpacing, 0, 3 * tileSpacing), Quaternion.identity, transform);
        generatedBoardTiles.Remove(randomShopTile);


        for (int i = 0; i < boardSize; i++) //column number of board
        {
            for (int j = 0; j < boardSize; j++) //row number of board
            {

                if (difficulty.NormalMode)
                {
                    if (i == 0 && j == 0) //always spawn empty tile at player spawnpoint
                    {
                        GameObject randomEmptyTile = generatedBoardTiles.Find(tile => tile.name == "Empty Tile"); //get a random empty tile from the list
                        tileList.Add(randomEmptyTile); //add tile for index searching
                        Debug.Log("Tile #" + currentTile + " in currentTile array is a " + randomEmptyTile.name);
                        currentTile++;
                        randomEmptyTile.transform.localScale = new Vector3(tileScaleFactor, tileScaleFactor, tileScaleFactor); //resize tile to fit board
                        GameObject emptyInstance = Instantiate(emptyTile, transform.position + new Vector3(i * tileSpacing, 0, j * tileSpacing), Quaternion.identity, transform);
                        emptyInstance.GetComponentInChildren<TileTrigger>().FlipTile();
                        generatedBoardTiles.Remove(randomEmptyTile);
                    }
                    else if ((i == 0 && j == 1) || (i == 1 && j == 0)) //always spawn item tiles adjacent to player start point
                    {

                        GameObject randomItemTile = generatedBoardTiles.Find(tile => tile.name == "Item Tile"); //get a random item tile from the list
                        tileList.Add(randomItemTile); //add tile for index searching
                        Debug.Log("Tile #" + currentTile + " in currentTile array is a " + randomItemTile.name);
                        currentTile++;
                        randomItemTile.transform.localScale = new Vector3(tileScaleFactor, tileScaleFactor, tileScaleFactor);//resize tile to fit board
                        GameObject itemTileInstance = Instantiate(itemTile, transform.position + new Vector3(i * tileSpacing, 0, j * tileSpacing), Quaternion.identity, transform);
                        generatedBoardTiles.Remove(randomItemTile);


                    }
                    else if (i == 4 && j == 3) //skip random instantiation on shop tile
                    {

                        tileList.Add(randomShopTile); //add tile for index searching
                        Debug.Log("Tile #" + currentTile + " in currentTile array is a " + randomShopTile.name);
                        currentTile++;
                        continue;
                    }
                    else if (i == winTileSlot && j == boardSize - 1) //skip random instantiation on winning tile
                    {
                        tileList.Add(winningTile);
                        Debug.Log("Tile #" + currentTile + " in currentTile array is a " + winningTile.name);
                        currentTile++;
                        continue;
                    }
                    else
                    {
                        int loopIterations = 0;
                        int randomTileIndex;
                        do
                        {
                            loopIterations++;
                            //get a random tile to spawn
                            randomTileIndex = UnityEngine.Random.Range(0, generatedBoardTiles.Count);

                            //if the random tile is a monster tile
                            if (generatedBoardTiles[randomTileIndex].name.Contains("Monster") == true)
                            {
                                bool leftTileIsMonster = false;
                                bool underTileIsMonster = false;
                                //check if a monster tile can be spawned (there is no monster tile adjacent = monster tile can be spawned)
                                if (i == 0) //checking in first row of board
                                {
                                    underTileIsMonster = tileList[currentTile - 1].name.Contains("Monster");//check if the previous square is monster tile or not
                                }
                                else if (i >= 1) //checking in all other rows of board
                                {
                                    underTileIsMonster = tileList[currentTile - 1].name.Contains("Monster");//check if the previous square is monster tile or not
                                    leftTileIsMonster = tileList[currentTile - 8].name.Contains("Monster");//check if the under square is monster tile or not
                                }

                                if (i == 0)
                                {
                                    if (!underTileIsMonster)
                                    {
                                        Debug.Log("encounter available for tile # " + currentTile + " ROW 1");
                                        break;
                                    }
                                }
                                else if (i >= 1)
                                {
                                    if (!leftTileIsMonster && !underTileIsMonster)
                                    {
                                        Debug.Log("encounter available for tile # " + currentTile + " OTHER ROW");
                                        break;
                                    }
                                }
                                //do something to break out if there's only monster tiles left in the list 
                                if (loopIterations > 5)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        while (true);

                        GameObject randomTile = generatedBoardTiles[randomTileIndex];
                        tileList.Add(randomTile);
                        Debug.Log("Tile #" + currentTile + " in currentTile array is a " + randomTile.name);
                        currentTile++;
                        GameObject randomTileInstance = Instantiate(randomTile, transform.position + new Vector3(i * tileSpacing, 0, j * tileSpacing), Quaternion.identity, transform);
                        randomTileInstance.transform.localScale = new Vector3(tileScaleFactor, tileScaleFactor, tileScaleFactor); //resize tile to fit board
                        generatedBoardTiles.Remove(randomTile);
                    }

                }
                else if (difficulty.HardMode)
                {
                    if (i == 0 && j == 0) //always spawn empty tile at player spawnpoint
                    {
                        GameObject randomEmptyTile = generatedBoardTiles.Find(tile => tile.name == "Empty Tile"); //get a random empty tile from the list
                        tileList.Add(randomEmptyTile); //add tile for index searching
                        Debug.Log("Tile #" + currentTile + " in currentTile array is a " + randomEmptyTile.name);
                        currentTile++;
                        randomEmptyTile.transform.localScale = new Vector3(tileScaleFactor, tileScaleFactor, tileScaleFactor); //resize tile to fit board
                        GameObject emptyInstance = Instantiate(emptyTile, transform.position + new Vector3(i * tileSpacing, 0, j * tileSpacing), Quaternion.identity, transform);
                        emptyInstance.GetComponentInChildren<TileTrigger>().FlipTile();
                        generatedBoardTiles.Remove(randomEmptyTile);
                    }
                    else if (i == winTileSlot && j == boardSize - 1) //skip random instantiation on winning tile
                    {
                        tileList.Add(winningTile);
                        Debug.Log("Tile #" + currentTile + " in currentTile array is a " + winningTile.name);
                        currentTile++;
                        continue;
                    }
                    else if (i == 4 && j == 3) //skip random instantiation on shop tile
                    {

                        tileList.Add(randomShopTile); //add tile for index searching
                        Debug.Log("Tile #" + currentTile + " in currentTile array is a " + randomShopTile.name);
                        currentTile++;
                        continue;
                    }
                    else
                    {
                        int randomTileIndex;
                        //get a random tile to spawn
                        randomTileIndex = UnityEngine.Random.Range(0, generatedBoardTiles.Count);


                        GameObject randomTile = generatedBoardTiles[randomTileIndex];
                        tileList.Add(randomTile);
                        Debug.Log("Tile #" + currentTile + " in currentTile array is a " + randomTile.name);
                        currentTile++;
                        GameObject randomTileInstance = Instantiate(randomTile, transform.position + new Vector3(i * tileSpacing, 0, j * tileSpacing), Quaternion.identity, transform);
                        randomTileInstance.transform.localScale = new Vector3(tileScaleFactor, tileScaleFactor, tileScaleFactor); //resize tile to fit board
                        generatedBoardTiles.Remove(randomTile);
                    }
                }
            }
        }
        //testing order of tiles
        // for (int i = 0; i < tileList.Count; i++)
        // {
        //     Debug.Log("Tile number " + i + "is " + tileList[i].name);
        // }
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
