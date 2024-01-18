using UnityEngine;
using System.Collections;

public class CardInteractLogic : MonoBehaviour
{
    private bool isSafeCard;
    private bool isClicked;
    public Color highlightColor = new Color(1f, 1f, 1f, 0.5f); // White with 50% opacity;
    public Color safeColor = Color.green;
    public Color deathColor = Color.red;
    public Color unflippedColor = Color.black;
    private MeshRenderer cardMesh;
    public IntegerReference playerHealth;

    private CardShufflerLogic cardShuffler; // Reference to the CardShufflerLogic script
    void Start()
    {
        isClicked = false;
        // Determine whether the card is safe or death (you can implement your logic here)
        isSafeCard = CheckIfSafeCard();

        //Assign card mesh for highlighting
        cardMesh = GetComponent<MeshRenderer>();

        //Start card with unflipped color
        cardMesh.material.color = unflippedColor;

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
        StartCoroutine(CardRemoveDelay());

        Debug.Log("Safe card! Your turn ends.");
        // Add logic for the player's turn ending (e.g., proceed to the next player's turn)
    }

    void HandleDeathCardInteraction()
    {
        //Chopping finger animation goes here
        HealthTest.Instance.ChangeHealth(-1);
        StartCoroutine(CardRemoveDelay());

        Debug.Log("Death card! You lose a finger!");
        // Add logic for the player's death (e.g., restart the game or show a game over screen)
    }
    void OnMouseEnter()
    {
        if (!isClicked)
        {
            cardMesh.material.color = highlightColor;
        }
    }
    void OnMouseExit()
    {
        if (!isClicked)
        {
            cardMesh.material.color = unflippedColor;
        }
    }
    void OnMouseUp()
    {
        Debug.Log("Mouse Test point 1." + Time.deltaTime);
        if (!isClicked)
        {
            isClicked = true;
            Debug.Log("Mouse Test point 2." + Time.deltaTime);
            if (isSafeCard)
            {
                HandleSafeCardInteraction();
            }
            else
            {
                HandleDeathCardInteraction();
            }
        }
    }
    private IEnumerator CardRemoveDelay()
    {
        if (isSafeCard)
        {
            cardMesh.material.color = safeColor;
        }
        else
        {
            cardMesh.material.color = deathColor;
        }
        yield return new WaitForSeconds(1);
        cardShuffler.RemoveCardFromTable(gameObject);
    }
}
