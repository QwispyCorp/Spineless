using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && transform.position.z < .08)
        {
            transform.Translate(0, 0, 0.04f);
        }
        else if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -.13)
        {
            transform.Translate(-.04f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < .14)
        {
            transform.Translate(.04f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S) && transform.position.z > -.17)
        {
            transform.Translate(0, 0, -0.04f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster Tile"))
        {
            Debug.Log("Player on Monster Tile");
            //Handle Encounter Stuff
        }
        else if (other.CompareTag("Item Tile"))
        {
            Debug.Log("Player on Item Tile");
            //Handle Item Stuff
        }
        else if (other.CompareTag("Empty Tile"))
        {
            Debug.Log("Player on EmptyTile");
            //Handle Empty Tile Stuff
        }
    }
}
