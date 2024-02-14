using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool tileEventTriggered;
    private bool choiceTileTriggered;
    private bool itemTileTriggered;
    private bool monsterTileTriggered;
    private GameObject collidedObject;
    [SerializeField] private GameObject playerInteractCanvas;
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private float tileSpacing; // for movement, changes based on board tile scaling
    public GameObject board;
    public Animator CameraAni;
    public bool playerOnBoard;
    private int wallLayerMask;
    void Start()
    {
        playerOnBoard = false;
        AudioManager.Instance.PlayMusicTrack("Encounter Music");
        playerInteractCanvas.SetActive(true);
        tileEventTriggered = false;
        itemTileTriggered = false;
        choiceTileTriggered = false;
        monsterTileTriggered = false;
        wallLayerMask = 1 << 7;
        tileSpacing = saveData.tileSpacing; //set tile spacing for movement equal to board tile spacing 
    }
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * tileSpacing, Color.green);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * tileSpacing, Color.green);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * tileSpacing, Color.green);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * tileSpacing, Color.green);
        if (!tileEventTriggered && playerOnBoard)
        {
            if (Input.GetKeyDown(KeyCode.W) && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), tileSpacing, wallLayerMask)) //later check for walls here
            {
                AudioManager.Instance.PlaySound("ChessPieceMove");
                transform.Translate(0, 0, tileSpacing);
            }
            else if (Input.GetKeyDown(KeyCode.A) && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), tileSpacing, wallLayerMask)) //later check for walls here
            {
                AudioManager.Instance.PlaySound("ChessPieceMove");
                transform.Translate(-tileSpacing, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.D) && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), tileSpacing, wallLayerMask)) //later check for walls here
            {
                AudioManager.Instance.PlaySound("ChessPieceMove");
                transform.Translate(tileSpacing, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.S) && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), tileSpacing, wallLayerMask)) //later check for walls here
            {
                AudioManager.Instance.PlaySound("ChessPieceMove");
                transform.Translate(0, 0, -tileSpacing);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        collidedObject = other.gameObject;

        if (other.CompareTag("Monster Tile") && playerOnBoard)
        {
            AudioManager.Instance.PlaySound("Riser");
            tileEventTriggered = true;
            monsterTileTriggered = true;
            HandleMonsterTile(collidedObject);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag("Item Tile") && playerOnBoard)
        {
            AudioManager.Instance.PlaySound("Riser");
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
            HandleWinTile(collidedObject);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag("Choice Tile") && playerOnBoard)
        {
            AudioManager.Instance.PlaySound("Riser");
            tileEventTriggered = true;
            choiceTileTriggered = true;
            HandleChoiceTile(collidedObject);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void HandleMonsterTile(GameObject tile)
    {
        tile.GetComponent<TileTrigger>().FlipTile();
        playerInteractCanvas.SetActive(false);
        Debug.Log("Player on Monster Tile");
        Invoke("SwitchRooms", 2);
    }
    private void HandleItemTile(GameObject tile)
    {
        tile.GetComponent<TileTrigger>().FlipTile();
        playerInteractCanvas.SetActive(false);
        Debug.Log("Player on Item Tile");
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
        Destroy(board);
        PopUpTextManager.Instance.ShowScreen("Win Screen");
        Debug.Log("Player on WinTile");
    }
    private void HandleChoiceTile(GameObject tile)
    {
        tile.GetComponent<TileTrigger>().FlipTile();
        playerInteractCanvas.SetActive(false);
        Debug.Log("Player on Choice Tile");
        Invoke("SwitchRooms", 2);

    }

    private void SwitchRooms()
    {
        BoardGenerator.Instance.HideBoard();

        CameraAni.SetTrigger("B2F");
        GameObject lightGameObject = GameObject.FindGameObjectWithTag("Light");
        if (lightGameObject != null)
        {
            LightManager lightManager = lightGameObject.GetComponent<LightManager>();

            if (lightManager != null)
            {
                if (choiceTileTriggered) //switch scene to choice room
                {
                    LightManager.Instance.StartFlickeringTransitionTo("ChoiceRoom");
                }
                else if (itemTileTriggered) //switch scene to item room
                {
                    LightManager.Instance.StartFlickeringTransitionTo("ItemRoom");
                }
                else if (monsterTileTriggered) //switch scene to encounter
                {
                    LightManager.Instance.StartFlickeringTransitionTo("Prototype");
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
