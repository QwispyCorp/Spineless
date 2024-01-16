using UnityEngine;

public class CardInteractLogic : MonoBehaviour
{
    private bool isSafeCard;
    private Color originalColor;
    public Color highlightColor = new Color(1f, 1f, 1f, 0.5f); // White with 50% opacity;

    void OnMouseEnter()
    {
        // This function is called when the mouse enters the card collider

        // Highlight the card when the mouse hovers over it
        HighlightCard(true);
    }

    void OnMouseExit()
    {
        // This function is called when the mouse exits the card collider

        // Remove the highlight when the mouse is no longer over the card
        HighlightCard(false);
    }

    void OnMouseDown()
    {
        // This function is called when the player clicks on the card

        if (isSafeCard)
        {
            HandleSafeCardInteraction();
        }
        else
        {
            HandleDeathCardInteraction();
        }
    }

    void Start()
    {
        // Determine whether the card is safe or death (you can implement your logic here)
        isSafeCard = CheckIfSafeCard();

        // Save the original color of the card for later use
        originalColor = GetComponent<Renderer>().material.color;

        // Ensure the cursor is always visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    void HighlightCard(bool highlight)
    {
        // Highlight or unhighlight the card based on the mouse hover state
        if (highlight)
        {
            // Change the color to the highlight color
            GetComponent<Renderer>().material.color = highlightColor;
        }
        else
        {
            // Use the original color obtained at the start
            GetComponent<Renderer>().material.color = originalColor;
        }
    }
}