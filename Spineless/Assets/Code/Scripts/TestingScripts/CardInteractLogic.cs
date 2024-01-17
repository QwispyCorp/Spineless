using UnityEngine;

public class CardInteractLogic : MonoBehaviour
{
    private bool isSafeCard;
    public Color highlightColor = new Color(1f, 1f, 1f, 0.5f); // White with 50% opacity;

    private CardShufflerLogic cardShuffler; // Reference to the CardShufflerLogic script
    void Start()
    {
        // Determine whether the card is safe or death (you can implement your logic here)
        isSafeCard = CheckIfSafeCard();

        // Ensure the cursor is always visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Find the CardShufflerLogic script in the scene
        cardShuffler = FindObjectOfType<CardShufflerLogic>();

        // If the script is not found, log an error
        if (cardShuffler == null)
        {
            Debug.LogError("CardShufflerLogic script not found in the scene. Make sure it's present in the scene.");
        }
    }

    bool CheckIfSafeCard()
    {
        // You may want to implement your logic to determine if this card is a safe card
        // For simplicity, let's say safe cards have the tag "SafeCard" and death cards have the tag "DeathCard"
        return gameObject.CompareTag("SafeCard");
    }

    void HandleSafeCardInteraction()
    {
        Debug.Log("Safe card! Your turn ends.");
        // Add logic for the player's turn ending (e.g., proceed to the next player's turn)
    }

    void HandleDeathCardInteraction()
    {
        Debug.Log("Death card! You die!");
        // Add logic for the player's death (e.g., restart the game or show a game over screen)
    }
    void OnMouseDown()
    {
        if (isSafeCard)
        {
            HandleSafeCardInteraction();
            cardShuffler.RemoveCardFromTable(gameObject);
        }
        else
        {
            HandleDeathCardInteraction();   
            cardShuffler.RemoveCardFromTable(gameObject);
        }
    }
}
