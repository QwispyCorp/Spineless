using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool tileEventTriggered;
    [SerializeField] private GameObject playerInteractCanvas;
    public GameObject board;
    public Animator CameraAni;
    public bool playerOnBoard;
    void Start()
    {
        playerOnBoard = false;
        AudioManager.Instance.PlayMusicTrack("Encounter Music");
        playerInteractCanvas.SetActive(true);
        tileEventTriggered = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!tileEventTriggered && playerOnBoard)
        {
            if (Input.GetKeyDown(KeyCode.W) && transform.position.z < .08) //later check for walls here
            {
                AudioManager.Instance.PlaySound("ChessPieceMove");
                transform.Translate(0, 0, 0.04f);
            }
            else if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -.13) //later check for walls here
            {
                AudioManager.Instance.PlaySound("ChessPieceMove");
                transform.Translate(-.04f, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < .13) //later check for walls here
            {
                AudioManager.Instance.PlaySound("ChessPieceMove");
                transform.Translate(.04f, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.S) && transform.position.z > -.17) //later check for walls here
            {
                AudioManager.Instance.PlaySound("ChessPieceMove");
                transform.Translate(0, 0, -0.04f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Monster Tile") && playerOnBoard)
        {
            AudioManager.Instance.PlaySound("Riser");
            tileEventTriggered = true;
            HandleMonsterTile(other.gameObject);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag("Item Tile") && playerOnBoard)
        {
            HandleItemTile(other.gameObject);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag("Empty Tile") && playerOnBoard)
        {
            HandleEmptyTile(other.gameObject);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag("Win Tile") && playerOnBoard)
        {
            HandleWinTile(other.gameObject);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void HandleMonsterTile(GameObject tile)
    {
        tile.GetComponent<TileTrigger>().FlipTile();
        playerInteractCanvas.SetActive(false);
        BoardGenerator.Instance.HideBoard();
        Debug.Log("Player on Monster Tile");
        Invoke("SwitchToEncounter", 2);
    }
    private void HandleItemTile(GameObject tile)
    {
        tile.GetComponent<TileTrigger>().FlipTile();
        Debug.Log("Player on Item Tile");
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

    private void SwitchToEncounter()
    {
        CameraAni.SetTrigger("Board2Free");
        GameObject lightGameObject = GameObject.FindGameObjectWithTag("Light");
        if (lightGameObject != null)
        {
            LightManager lightManager = lightGameObject.GetComponent<LightManager>();

            if (lightManager != null)
            {
                LightManager.Instance.StartCoroutine(lightManager.StartFlickeringTransition());
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
