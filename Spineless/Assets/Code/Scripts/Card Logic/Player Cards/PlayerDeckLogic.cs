using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDeckLogic : MonoBehaviour
{
    public List<GameObject> safeCardModels; // List of safe card models
    public List<GameObject> deathCardModels; // List of death card models
    public List<GameObject> jokerCardModels; // List of joker card models
    public GameObject cardSpawnPoint; // Drag your custom pivot point object here in the Inspector
    private List<GameObject> deck; // The deck of cards
    private List<GameObject> tableCards; // The cards on the table
    [Header("Player Deck Properties")]
    [SerializeField] private int totalCardsAmount;
    [SerializeField] private int totalDeathCards;
    [SerializeField] private int totalJokerCards;
    [SerializeField] private float cardSpacing;

    [Header("Game Data")]
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private EncounterData encounterData;
    public delegate void CardsUpdater();
    public static event CardsUpdater PlayerCardsUpdated;

    void Awake()
    {
        deck = new List<GameObject>();
        tableCards = new List<GameObject>();

    }
    void Start()
    {
        InitializeDeck();
        ShuffleDeck();
        DrawHand();
    }

    void InitializeDeck()
    {
        int deathCardsIn = 0;
        int jokerCardsIn = 0;

        for (int i = 0; i < totalCardsAmount; i++)
        {
            if (deathCardsIn < totalDeathCards)
            {
                deck.Add(GetRandomCardModel(deathCardModels)); // Add random death card model
                deathCardsIn++;
            }
            else if (jokerCardsIn < totalJokerCards)
            {
                deck.Add(GetRandomCardModel(jokerCardModels)); // Add random death card model
                jokerCardsIn++;
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
    }
    void UpdateEncounterCards()
    {
        encounterData.PlayerDeck = deck;
        encounterData.PlayerTableCards = tableCards;
        if (PlayerCardsUpdated != null)
        {
            PlayerCardsUpdated?.Invoke();
        }
    }

    public void DrawHand()
    {

        // Draw and instantiate 5 cards in front of the player on the table
        for (int i = 0; i < 5; i++)
        {
            DrawCard();
        }
        AudioManager.Instance.PlaySound("CardFlip4");
        UpdateEncounterCards();
    }
    public void RemoveDeathCard()
    {
        if (tableCards.Find(x => x.gameObject.name == "PlayerDeathCard(Clone)"))
        {
            RemoveCardFromTable(tableCards.Find(x => x.gameObject.name == "PlayerDeathCard(Clone)"));
            UpdateEncounterCards();

        }
        else
        {
            Debug.Log("Could not find card named " + tableCards.Find(x => x.gameObject.name == "PlayerDeathCard(Clone)"));
        }
    }
    public void ShowAllCards()
    {
        for (int i = 0; i < tableCards.Count; i++)
        {
            tableCards[i].GetComponent<PlayerCardInteraction>().ShowCard();
        }
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
                //Update encounter cards
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