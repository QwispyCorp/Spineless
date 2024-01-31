using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool tileEventTriggered;
    public Animator CameraAni;
    void Start()
    {
        tileEventTriggered = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!tileEventTriggered)
        {
            if (Input.GetKeyDown(KeyCode.W) && transform.position.z < .08)
            {
                transform.Translate(0, 0, 0.04f);
            }
            else if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -.13)
            {
                transform.Translate(-.04f, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < .13)
            {
                transform.Translate(.04f, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.S) && transform.position.z > -.17)
            {
                transform.Translate(0, 0, -0.04f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster Tile"))
        {
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
    }

    private void HandleMonsterTile(GameObject tile)
    {
        tile.GetComponent<TileTrigger>().FlipTile();
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
