using UnityEngine;

public class CardInteractLogic : MonoBehaviour
{
    private bool isSafeCard;

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
}
