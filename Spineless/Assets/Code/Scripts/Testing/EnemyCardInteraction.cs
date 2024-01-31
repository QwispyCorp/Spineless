using UnityEngine;
using System.Collections;

public class EnemyCardInteraction : MonoBehaviour
{
    private bool isSafeCard;
    public Color highlightColor = new Color(1f, 1f, 1f, 0.5f); // White with 50% opacity;
    public Color safeColor = Color.green;
    public Color deathColor = Color.red;
    public Color unflippedColor = Color.black;
    private MeshRenderer cardMesh;
    public IntegerReference enemyHealth;

    private EnemyDeckLogic enemyDeck; // Reference to the enemyDeckLogic script
    [SerializeField] private float cardRemoveDelayTime;
    void Start()
    {
        // Determine whether the card is safe or death (you can implement your logic here)
        isSafeCard = CheckIfSafeCard();

        //Assign card mesh for highlighting
        cardMesh = GetComponent<MeshRenderer>();

        //Start card with unflipped color
        cardMesh.material.color = unflippedColor;

        // Ensure the cursor is always visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Find the enemyDeckLogic script in the scene
        enemyDeck = FindObjectOfType<EnemyDeckLogic>();

        // If the script is not found, log an error
        if (enemyDeck == null)
        {
            Debug.LogError("enemyDeckLogic script not found in the scene. Make sure it's present in the scene.");
        }
    }

    public bool CheckIfSafeCard()
    {
        // You may want to implement your logic to determine if this card is a safe card
        // For simplicity, let's say safe cards have the tag "SafeCard" and death cards have the tag "DeathCard"
        return gameObject.CompareTag("SafeCard");
    }

    void HandleSafeCardInteraction()
    {
        cardMesh.material.color = safeColor; //change card color to safe card color
        PopUpTextManager.Instance.ShowScreen("Safe Card Screen"); //show safe screen 
        StartCoroutine(CardRemoveDelay());
        Debug.Log("Enemy safe card!");
    }

    void HandleDeathCardInteraction()
    {
        //Chopping finger animation goes here
        EnemyHealthTest.Instance.ChangeHealth(-1); //Decrease health
        cardMesh.material.color = deathColor; //change card color to death card color
        PopUpTextManager.Instance.ShowScreen("Death Card Screen"); //show death screen 
        StartCoroutine(CardRemoveDelay());
        Debug.Log("Enemy Death Card!");
    }
    public void EnemyCardSelected()
    {
        //when this card is selected by the enemy AI
        if (isSafeCard) //if the card is safe
        {
            HandleSafeCardInteraction(); //handle safe card logic
        }
        else //otherwise
        {
            HandleDeathCardInteraction(); //handle death card logic
        }
    }
    private void SwitchState()
    {
        enemyDeck.RemoveCardFromTable(gameObject); //remove the card from the table
        StateTest.Instance.UpdateEncounterState(StateTest.EncounterState.PlayerTurn);

        //Check if table is empty
        if (enemyDeck.CheckTableCards() == 0)
        {
            //if table is empty after flipping a card, redraw all cards
            enemyDeck.DrawHand();
        }
    }

    private IEnumerator CardRemoveDelay()
    {
        yield return new WaitForSeconds(cardRemoveDelayTime); //wait for the delay time before removing the card from the table
        //PopUpTextManager.Instance.CloseScreen(PopUpTextManager.Instance.currentScreen); //close chosen card screen overlay
        Invoke("SwitchState", cardRemoveDelayTime);

    }
}
