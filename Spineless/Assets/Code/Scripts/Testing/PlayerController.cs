using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool tileEventTriggered;
    [SerializeField] private GameObject playerInteractCanvas;
    public Animator CameraAni;
    void Start()
    {
        AudioManager.Instance.PlayMusicTrack("Encounter Music");
        playerInteractCanvas.SetActive(true);
        tileEventTriggered = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!tileEventTriggered)
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

        other.gameObject.GetComponent<BoxCollider>().enabled = false;

        if (other.CompareTag("Monster Tile"))
        {
            AudioManager.Instance.PlaySound("Riser");
            tileEventTriggered = true;
            HandleMonsterTile(other.gameObject);
        }
        else if (other.CompareTag("Item Tile"))
        {
            HandleItemTile(other.gameObject);
        }
        else if (other.CompareTag("Empty Tile"))
        {
            HandleEmptyTile(other.gameObject);
        }
        else if (other.CompareTag("Win Tile"))
        {
            HandleWinTile(other.gameObject);
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
}
