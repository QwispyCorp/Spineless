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
    [SerializeField] private float animationDuration = 1;
    public GameObject board;
    public Animator CameraAni;
    public Animator Pawn;
    public bool playerOnBoard;
    private int wallLayerMask;
    private bool isMoving;
    private bool movingUp;
    private bool movingDown;
    private bool movingLeft;
    private bool movingRight;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float slideTimer;
    [SerializeField] private AudioSource[] pieceSounds;
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
        movingUp = false;
        movingDown = false;
        movingLeft = false;
        movingRight = false;
        slideTimer = 0;

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
            if (Input.GetKeyDown(KeyCode.W) && !isMoving && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), tileSpacing, wallLayerMask)) //later check for walls here
            {
                int randomSoundIndex = UnityEngine.Random.Range(0, pieceSounds.Length);
                pieceSounds[randomSoundIndex].Play();
                initialPosition = transform.position;
                targetPosition = initialPosition + transform.forward * tileSpacing;
                isMoving = true;
            }
            else if (Input.GetKeyDown(KeyCode.A) && !isMoving && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), tileSpacing, wallLayerMask)) //later check for walls here
            {
                int randomSoundIndex = UnityEngine.Random.Range(0, pieceSounds.Length);
                pieceSounds[randomSoundIndex].Play();
                initialPosition = transform.position;
                targetPosition = initialPosition + Vector3.left * tileSpacing;
                isMoving = true;
            }
            else if (Input.GetKeyDown(KeyCode.D) && !isMoving && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), tileSpacing, wallLayerMask)) //later check for walls here
            {
                int randomSoundIndex = UnityEngine.Random.Range(0, pieceSounds.Length);
                pieceSounds[randomSoundIndex].Play();
                initialPosition = transform.position;
                targetPosition = initialPosition + Vector3.right * tileSpacing;
                isMoving = true;
            }
            else if (Input.GetKeyDown(KeyCode.S) && !isMoving && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), tileSpacing, wallLayerMask)) //later check for walls here
            {
                int randomSoundIndex = UnityEngine.Random.Range(0, pieceSounds.Length);
                pieceSounds[randomSoundIndex].Play();
                initialPosition = transform.position;
                targetPosition = initialPosition + Vector3.back * tileSpacing;
                isMoving = true;
            }
        }
        if (isMoving)
        {
            slideTimer += Time.deltaTime;
            float t = Mathf.Clamp01(slideTimer / animationDuration);
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            Debug.Log("Piece animation percentage: " + t);
            if (t >= 1)
            {
                isMoving = false;
                slideTimer = 0f;
                saveData.lastPlayerTransform = transform.position;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        collidedObject = other.gameObject;

        if (other.CompareTag("Monster Tile") && playerOnBoard)
        {
            AudioManager.Instance.PlaySound("TileEvent");
            tileEventTriggered = true;
            monsterTileTriggered = true;
            HandleMonsterTile(collidedObject);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag("Item Tile") && playerOnBoard)
        {
            AudioManager.Instance.PlaySound("TileEvent");
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
                tileEventTriggered = true;
                shopTileTriggered = true;
                HandleShopTile(collidedObject);
                //other.gameObject.GetComponent<BoxCollider>().enabled = false;
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
        Invoke("SwitchRooms", 3);
    }
    private void HandleItemTile(GameObject tile)
    {
        tile.GetComponent<TileTrigger>().FlipTile();
        playerInteractCanvas.SetActive(false);
        Debug.Log("Player on Item Tile");
        Pawn.SetTrigger("Move");
        Invoke("SwitchRooms", 3);
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
        Invoke("SwitchRooms", 3);
    }
    private void HandleShopTile(GameObject tile)
    {
        tile.GetComponent<TileTrigger>().FlipTile();
        playerInteractCanvas.SetActive(false);
        Debug.Log("Player on Shop Tile");
        Pawn.SetTrigger("Move");
        Invoke("SwitchRooms", 3);
    }

    private void MovePawn()
    {
        Pawn.SetTrigger("Move");
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
