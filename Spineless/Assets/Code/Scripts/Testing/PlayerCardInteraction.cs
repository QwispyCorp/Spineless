using UnityEngine;
using System.Collections;

public class PlayerCardInteraction : MonoBehaviour
{
    private bool isSafeCard;
    private bool isClicked;
    public Color highlightColor = new Color(1f, 1f, 1f, 0.5f); // White with 50% opacity;
    public Color safeColor = Color.green;
    public Color deathColor = Color.red;
    public Color unflippedColor = Color.black;
    private MeshRenderer cardMesh;
    public IntegerReference playerHealth;

    private PlayerDeckLogic playerDeck; // Reference to the playerDeckLogic script
    [SerializeField]
    private float cardRemoveDelayTime;
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

        // Find the playerDeckLogic script in the scene
        playerDeck = FindObjectOfType<PlayerDeckLogic>();

        // If the script is not found, log an error
        if (playerDeck == null)
        {
            Debug.LogError("playerDeckLogic script not found in the scene. Make sure it's present in the scene.");
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
        cardMesh.material.color = safeColor; //change card color to safe color
        PopUpTextManager.Instance.ShowScreen("Safe Card Screen"); //show safe screen overlay
        StartCoroutine(CardRemoveDelay()); //start coroutine to remove card and screen overlay
        Debug.Log("Safe card! Your turn ends.");
    }

    void HandleDeathCardInteraction()
    {
        //Chopping finger animation goes here
        PlayerHealthTest.Instance.ChangeHealth(-1); //Decrease health
        cardMesh.material.color = deathColor; //change card color to death color
        PopUpTextManager.Instance.ShowScreen("Death Card Screen"); //show death screen overlay
        StartCoroutine(CardRemoveDelay()); //start coroutine to remove card and screen overlay
        Debug.Log("Death card! You lose a finger!");
    }
    void OnMouseEnter()
    {
        if (!isClicked && StateTest.Instance.CurrentEncounterState == StateTest.EncounterState.PlayerTurn)
        {
            cardMesh.material.color = highlightColor;
        }
    }
    void OnMouseExit()
    {
        if (!isClicked && StateTest.Instance.CurrentEncounterState == StateTest.EncounterState.PlayerTurn)
        {
            cardMesh.material.color = unflippedColor;
        }
    }
    void OnMouseUp()
    {
        if (isClicked == false && StateTest.Instance.CurrentEncounterState == StateTest.EncounterState.PlayerTurn)
        {
            StateTest.Instance.UpdateEncounterState(StateTest.EncounterState.PlayerSafe);
            isClicked = true;

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
    private void SwitchState()
    {
        playerDeck.RemoveCardFromTable(gameObject); //remove the card from the table

        //Check if table is empty
        if (playerDeck.CheckTableCards() == 0)
        {
            //if table is empty after flipping a card, redraw all cards
            playerDeck.DrawHand();
        }
        StateTest.Instance.UpdateEncounterState(StateTest.EncounterState.EnemyTurn);

    }
    private IEnumerator CardRemoveDelay()
    {
        yield return new WaitForSeconds(cardRemoveDelayTime); //wait for the delay time before removing the card from the table
        PopUpTextManager.Instance.CloseScreen(PopUpTextManager.Instance.currentScreen); //close chosen card screen overlay
        Invoke("SwitchState", cardRemoveDelayTime);
    }
}
