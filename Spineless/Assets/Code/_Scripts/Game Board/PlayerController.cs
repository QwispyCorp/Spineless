using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private bool tileEventTriggered;
    private bool shopTileTriggered;
    private bool itemTileTriggered;
    private bool winTileTriggered;
    private bool monsterTileTriggered;
    private GameObject collidedObject;
    [SerializeField] private GameObject playerInteractCanvas;
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private float tileSpacing; // for movement, changes based on board tile scaling
    public GameObject board;
    public Animator CameraAni;
    public Animator Pawn;
    public bool playerOnBoard;
    private int wallLayerMask;
    void Start()
    {
        playerOnBoard = false;
        //AudioManager.Instance.PlayMusicTrack("Encounter Music");
        playerInteractCanvas.SetActive(true);
        tileEventTriggered = false;
        itemTileTriggered = false;
        shopTileTriggered = false;
        monsterTileTriggered = false;
        wallLayerMask = 1 << 7;
        tileSpacing = saveData.TileSpacing; //set tile spacing for movement equal to board tile spacing 

        //STARTING POSITION
        if (!saveData.HasEnteredBoardRoom) //if player enters gameboard for first time
        {
            transform.position = saveData.playerStartTransform;
            saveData.HasEnteredBoardRoom = true;
        }
        else
        {
            transform.position = saveData.lastPlayerTransform;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!tileEventTriggered && playerOnBoard)
        {
            if (Input.GetKeyDown(KeyCode.W) && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), tileSpacing, wallLayerMask)) //later check for walls here
            {
                AudioManager.Instance.PlaySound("ChessPieceMove");
                //Pawn.SetTrigger("W");
                transform.Translate(0, 0, tileSpacing);
            }
            else if (Input.GetKeyDown(KeyCode.A) && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), tileSpacing, wallLayerMask)) //later check for walls here
            {
                AudioManager.Instance.PlaySound("ChessPieceMove");
                //Pawn.SetTrigger("A");
                transform.Translate(-tileSpacing, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.D) && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), tileSpacing, wallLayerMask)) //later check for walls here
            {
                AudioManager.Instance.PlaySound("ChessPieceMove");
                //Pawn.SetTrigger("D");
                transform.Translate(tileSpacing, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.S) && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), tileSpacing, wallLayerMask)) //later check for walls here
            {
                AudioManager.Instance.PlaySound("ChessPieceMove");
                //Pawn.SetTrigger("S");
                transform.Translate(0, 0, -tileSpacing);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        collidedObject = other.gameObject;

        if (other.CompareTag("Monster Tile") && playerOnBoard)
        {
            AudioManager.Instance.PlaySound("TileEvent");

            saveData.lastPlayerTransform = transform.position;
            tileEventTriggered = true;
            monsterTileTriggered = true;
            HandleMonsterTile(collidedObject);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag("Item Tile") && playerOnBoard)
        {
            AudioManager.Instance.PlaySound("TileEvent");
            saveData.lastPlayerTransform = transform.position;
            tileEventTriggered = true;
            itemTileTriggered = true;
            HandleItemTile(collidedObject);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag("Empty Tile") && playerOnBoard)
        {
            HandleEmptyTile(collidedObject);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag("Win Tile") && playerOnBoard)
        {
            AudioManager.Instance.PlaySound("TileEvent");
            saveData.lastPlayerTransform = transform.position;
            tileEventTriggered = true;
            winTileTriggered = true;
            HandleWinTile(collidedObject);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag("Shop Tile") && playerOnBoard)
        {
            if (saveData.ShopVisited == false)
            {
                AudioManager.Instance.PlaySound("Riser");
                saveData.lastPlayerTransform = transform.position;
                tileEventTriggered = true;
                shopTileTriggered = true;
                HandleShopTile(collidedObject);
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                //player cannot re-enter shop feedback
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Win Tile"))
        {
            PopUpTextManager.Instance.CloseAllScreens();
        }
    }

    private void HandleMonsterTile(GameObject tile)
    {
        tile.GetComponent<TileTrigger>().FlipTile();
        playerInteractCanvas.SetActive(false);
        Debug.Log("Player on Monster Tile");
        Pawn.SetTrigger("Move");
        Invoke("SwitchRooms", 2);
    }
    private void HandleItemTile(GameObject tile)
    {
        tile.GetComponent<TileTrigger>().FlipTile();
        playerInteractCanvas.SetActive(false);
        Debug.Log("Player on Item Tile");
        Pawn.SetTrigger("Move");
        Invoke("SwitchRooms", 2);
    }
    private void HandleEmptyTile(GameObject tile)
    {
        tile.GetComponent<TileTrigger>().FlipTile();
        Debug.Log("Player on EmptyTile");
    }
    private void HandleWinTile(GameObject tile)
    {
        tile.GetComponent<TileTrigger>().FlipTile();
        playerInteractCanvas.SetActive(false);
        Debug.Log("Player on WinTile");
        Pawn.SetTrigger("Move");
        Invoke("SwitchRooms", 2);
    }
    private void HandleShopTile(GameObject tile)
    {
        tile.GetComponent<TileTrigger>().FlipTile();
        playerInteractCanvas.SetActive(false);
        Debug.Log("Player on Shop Tile");
        Pawn.SetTrigger("Move");
        Invoke("SwitchRooms", 2);

    }

    private void SwitchRooms()
    {
        AudioManager.Instance.PlaySound("Riser");
        BoardGenerator.Instance.HideBoard();

        CameraAni.SetTrigger("B2F");
        GameObject lightGameObject = GameObject.FindGameObjectWithTag("Light");
        if (lightGameObject != null)
        {
            LightManager lightManager = lightGameObject.GetComponent<LightManager>();

            if (lightManager != null)
            {
                if (shopTileTriggered) //switch scene to shop room
                {
                    LightManager.Instance.StartFlickeringTransitionTo("ShopRoom");
                }
                else if (itemTileTriggered) //switch scene to item room
                {
                    LightManager.Instance.StartFlickeringTransitionTo("ItemRoom");
                }
                else if (monsterTileTriggered && saveData.FirstEncounterEntered) //switch scene to encounter
                {
                    LightManager.Instance.StartFlickeringTransitionTo("Encounter");
                }
                else if (monsterTileTriggered && !saveData.FirstEncounterEntered) //switch scene to encounter tutorial if player has not yet entered an encounter
                {
                    LightManager.Instance.StartFlickeringTransitionTo("EncounterTutorial");
                }
                else if (winTileTriggered) //switch to winning scene for ending animations
                {
                    LightManager.Instance.StartFlickeringTransitionTo("WinScene");
                }
            }
        }
    }
    public void PlayerOnBoard()
    {
        playerOnBoard = true;
    }
    public void PlayerOffBoard()
    {
        playerOnBoard = false;
    }
}
