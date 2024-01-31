using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTrigger : MonoBehaviour

{
    [SerializeField] private GameObject displayedTile;
    [SerializeField] private GameObject coveredTile;
    void Start()
    {
        coveredTile.SetActive(true);
        displayedTile.SetActive(false);
    }
    public void FlipTile()
    {
        Debug.Log("In Tile trigger Collision");
        coveredTile.SetActive(false);
        displayedTile.SetActive(true);
    }
}
