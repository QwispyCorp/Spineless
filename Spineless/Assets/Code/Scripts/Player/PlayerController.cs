using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool tileEventTriggered;
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
            HandleMonsterTile(collidedObject);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag("Item Tile") && playerOnBoard)
        {
            tileEventTriggered = true;
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
            HandleChoiceTile(collidedObject);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void HandleMonsterTile(GameObject tile)
    {
        tile.GetComponent<TileTrigger>().FlipTile();
        playerInteractCanvas.SetActive(false);
        Debug.Log("Player on Monster Tile");
        Invoke("SwitchToEncounter", 2);
    }
    private void HandleItemTile(GameObject tile)
    {
        tile.GetComponent<TileTrigger>().FlipTile();
        playerInteractCanvas.SetActive(false);
        Debug.Log("Player on Item Tile");
        Invoke("CollectItem", 2);
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
        Debug.Log("Player on Choice Tile");
    }

    private void SwitchToEncounter()
    {
        BoardGenerator.Instance.HideBoard();

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

    private void CollectItem()
    {
        int randomItemIndex = UnityEngine.Random.Range(0, saveData.ItemPool.Count);
        string foundItemName = saveData.ItemPool[randomItemIndex].itemName;
        bool playerHasItem = saveData.EquippedItems.Find(x => x.itemName == foundItemName);
        if (saveData.ItemPool.Count > 0) //if list of available items is not empty
        {
            if (playerHasItem) //if player already has this item
            {
                // collidedObject.GetComponent<BoxCollider>().enabled = true; re-enable colider for later collection
                PopUpTextManager.Instance.ShowScreen("Item Already Equipped Screen");
                Array.Find(PopUpTextManager.Instance.Screens, screen => screen.name == "Item Already Equipped Screen").canvas.GetComponentInChildren<TextMeshProUGUI>().SetText(foundItemName + " already equipped!");
                Debug.Log("Too bad! you already have a " + saveData.ItemPool[randomItemIndex].itemName);
            }
            else
            {
                PopUpTextManager.Instance.ShowScreen(saveData.ItemPool[randomItemIndex].itemName + " Collected Screen");
                AudioManager.Instance.PlaySound(saveData.ItemPool[randomItemIndex].itemName);
                saveData.EquippedItems.Add(saveData.ItemPool[randomItemIndex]); //add item to equipped items
                Debug.Log(saveData.ItemPool[randomItemIndex].itemName + " Collected");
            }
        }
        tileEventTriggered = false;
        playerInteractCanvas.SetActive(true);
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
