using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDeckLogic : MonoBehaviour
{
    public Transform enemyTransform; // Assign the player's transform in the Inspector
    public List<GameObject> safeCardModels; // List of safe card models
    public List<GameObject> deathCardModels; // List of death card models
    public GameObject cardSpawnPoint; // Drag your custom pivot point object here in the Inspector
    private List<GameObject> deck; // The deck of cards
    private List<GameObject> tableCards; // The cards on the table
    [Header("Enemy Deck Properties")]
    [SerializeField] private int totalCardsAmount;
    [SerializeField] private int totalDeathCards;
    [SerializeField] private int totalJokerCards;
    [SerializeField] private float cardSpacing;
    [Header("Game Data")]
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private EncounterData encounterData;
    public delegate void EnemyCardsUpdater();
    public static event EnemyCardsUpdater EnemyCardsUpdated;


    void Awake()
    {
        deck = new List<GameObject>();
        tableCards = new List<GameObject>();

    }
    void Start()
    {
        InitializeDeck();
        ShuffleDeck();
        RefreshTable();
        DrawHand();
    }

    void InitializeDeck()
    {
        //deck = new List<GameObject>();
        int totalCards = 52;
        int deathCardCount = totalCards / 4;

        for (int i = 0; i < totalCards; i++)
        {
            if (i < deathCardCount)
            {
                deck.Add(GetRandomCardModel(deathCardModels)); // Add random death card model
            }
            else
            {
                deck.Add(GetRandomCardModel(safeCardModels)); // Add random safe card model
            }
        }
    }

    void ShuffleDeck()
    {
        // Shuffle the deck using Fisher-Yates algorithm
        for (int i = deck.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }

        UpdateEncounterCards();
    }
    void UpdateEncounterCards()
    {
        encounterData.EnemyDeck = deck;
        encounterData.EnemyTableCards = tableCards;
        if (EnemyCardsUpdated != null)
        {
            EnemyCardsUpdated?.Invoke();
        }
    }
    public void RefreshTable()
    {
        // Remove all cards from the table
        foreach (GameObject card in tableCards)
        {
            Destroy(card);
        }
        tableCards.Clear(); // Clear the list
    }
    public void DrawHand()
    {
        //tableCards = new List<GameObject>();

        // Draw and instantiate 5 cards in front of the player on the table
        for (int i = 0; i < 5; i++)
        {
            DrawCard();
        }

        UpdateEncounterCards();
    }

    public void EnemyCardSelection()
    {
        //StateTest.Instance.DelayTurn();
        AudioManager.Instance.PlaySound("CardFlip" + Random.Range(1, 3).ToString());
        int randomCardIndex = Random.Range(0, tableCards.Count);
        GameObject chosenCard;
        bool cardExists = false;

        while (cardExists == false)
        {
            randomCardIndex = Random.Range(0, tableCards.Count);

            chosenCard = tableCards[randomCardIndex];

            if (chosenCard != null)
            {
                cardExists = true;
                chosenCard.GetComponent<EnemyCardInteraction>().EnemyCardSelected();
            }
        }

        UpdateEncounterCards();
    }

    void DrawCard()
    {
        // Check if there are cards left in the deck and if the maximum number of cards hasn't been reached
        if (deck.Count > 0 && tableCards.Count < 5)
        {
            // Draw a card from the deck
            GameObject drawnCard = deck[0];
            deck.RemoveAt(0);

            // Use the custom pivot object for accurate positioning
            if (cardSpawnPoint != null)
            {
                Vector3 spawnPosition = cardSpawnPoint.transform.position + new Vector3(tableCards.Count * cardSpacing, 0f, 0f);

                GameObject card = Instantiate(drawnCard, spawnPosition, Quaternion.identity);
                //card.AddComponent<CardInteractLogic>(); // Add CardInteractLogic script to each card

                // Add the card to the list of table cards
                tableCards.Add(card);


                UpdateEncounterCards();
            }
            else
            {
                Debug.LogError("Custom pivot object not assigned.");
            }
        }
        else
        {
            Debug.Log("No cards left in the deck.");
        }
    }

    // Function to draw an additional card and add it to the existing cards on the table
    public void DrawAdditionalCard()
    {
        DrawCard(); // Draw a single card and add it to the table
    }

    GameObject GetRandomCardModel(List<GameObject> cardModelList)
    {
        // Get a random card model from the specified list
        int randomIndex = Random.Range(0, cardModelList.Count);
        return cardModelList[randomIndex];
    }

    // Function to add a card dynamically
    public void AddCard(bool isSafe)
    {
        List<GameObject> cardModels = isSafe ? safeCardModels : deathCardModels;
        GameObject newCard = Instantiate(GetRandomCardModel(cardModels), Vector3.zero, Quaternion.identity);
        tableCards.Add(newCard);// Add the new card to the list of table cards
    }
    public void RemoveCardFromTable(GameObject card) //------REMOVES CARD FROM DRAWN SELECTION
    {
        // Check if the card is in the tableCards list
        if (tableCards.Contains(card))
        {
            // Remove the card from the list
            tableCards.Remove(card);
            UpdateEncounterCards();
            // Destroy the card GameObject
            Destroy(card);
        }
    }
    public int CheckTableCards()
    {
        return tableCards.Count;
    }
}