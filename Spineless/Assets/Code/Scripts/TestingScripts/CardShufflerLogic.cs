using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShufflerLogic : MonoBehaviour
{
    public Transform playerTransform; // Assign the player's transform in the Inspector
    public Transform tableTransform;  // Assign the table's transform in the Inspector
    public List<GameObject> safeCardModels; // List of safe card models
    public List<GameObject> deathCardModels; // List of death card models

    void Start()
    {
        ShuffleDeck();
    }

    void ShuffleDeck()
    {
        List<GameObject> deck = new List<GameObject>();

        // Create a deck with 1/6th death cards and the rest safe cards for testing purposes
        int totalCards = 52;
        int deathCardCount = totalCards / 6;

        for (int i = 0; i < totalCards; i++)
        {
            if (i < deathCardCount)
            {
                deck.Add(GetRandomCardModel(deathCardModels)); 
            }
            else
            {
                deck.Add(GetRandomCardModel(safeCardModels)); 
            }
        }

        // Shuffle the deck using Fisher-Yates algorithm (did this with chatGPT)
        for (int i = deck.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }

        // Position 7 shuffled cards in front of the player on the table
        float cardSpacing = 1.5f; // Adjust this value for card spacing
        Vector3 spawnPosition = tableTransform.position;

        for (int i = 0; i < 7; i++)
        {
            Vector3 offset = new Vector3(i * cardSpacing, 0f, 0f);
            GameObject card = Instantiate(deck[i], spawnPosition + offset, Quaternion.identity);
            card.AddComponent<CardInteractLogic>(); // Adds CardInteraction script to each card
        }
    }

    GameObject GetRandomCardModel(List<GameObject> cardModelList)
    {
        // Gets a random card model from the specified list
        int randomIndex = Random.Range(0, cardModelList.Count);
        return cardModelList[randomIndex];
    }
}