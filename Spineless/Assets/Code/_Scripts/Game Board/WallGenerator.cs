using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    [SerializeField] private BoardGenerator board;
    [SerializeField] private GameObject[] wallLayouts;
    private GameObject chosenLayout;

    void Start()
    {
        if (!board.boardGenerated) //if board hasnt been generated, then choose a wall layout and activate it
        {
            ChooseWallLayout();
            Debug.Log("Spawning wall layout: " + chosenLayout.name);
            Instantiate(chosenLayout, transform);
        }
    }

    private void ChooseWallLayout()
    {
        int randomWallIndex = Random.Range(0, wallLayouts.Length - 1);
        chosenLayout = wallLayouts[randomWallIndex];
    }
}
