using UnityEngine;
using System.Collections;

public class PlayerCardInteraction : MonoBehaviour
{
    private bool isSafeCard;
    private bool isJokerCard;
    private bool isClicked;
    public Color highlightColor = new Color(1f, 1f, 1f, 0.5f); // White with 50% opacity;
    public Color unHighlightColor = Color.black; // White with 50% opacity;
    public Material cardBackMaterial;
    public Material safeMaterial;
    public Material deathMaterial;
    public Material jokerMaterial;
    public Color safeColor = Color.green;
    public Color deathColor = Color.red;
    public Color jokerColor = Color.yellow;
    public Color unflippedColor = Color.black;
    private MeshRenderer cardMesh;

    private PlayerDeckLogic playerDeck; // Reference to the playerDeckLogic script
    [SerializeField] private float cardRemoveDelayTime;
    void Start()
    {
        isClicked = false;
        // Determine whether the card is safe or death or joker (you can implement your logic here)
        isSafeCard = CheckIfSafeCard();
        isJokerCard = CheckIfJokerCard();

        //Assign card mesh for highlighting
        cardMesh = GetComponent<MeshRenderer>();

        //Start card with unflipped color
        //cardMesh.material.color = unflippedColor;
        cardMesh.material = cardBackMaterial;

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
    bool CheckIfJokerCard()
    {
        return gameObject.CompareTag("JokerCard");
    }

    void HandleSafeCardInteraction()
    {
        //cardMesh.material.color = safeColor; //change card color to safe color
        cardMesh.material = safeMaterial;
        PopUpTextManager.Instance.ShowScreen("Safe Card Screen"); //show safe screen overlay
        StartCoroutine(CardRemoveDelay()); //start coroutine to remove card and screen overlay
        Debug.Log("Safe card! Your turn ends.");
    }
    public void ShowCard()
    {
        if (isSafeCard)
        {
            cardMesh.material = safeMaterial;
        }
        else if (isJokerCard)
        {
            cardMesh.material = jokerMaterial;
        }
        else
        {
            cardMesh.material = deathMaterial;
        }
    }

    void HandleDeathCardInteraction()
    {
        //Chopping finger animation goes here
        AudioManager.Instance.PlaySound("SeveredHand");
        PlayerHealthTest.Instance.ChangeHealth(-1); //Decrease health
        //cardMesh.material.color = deathColor; //change card color to death color
        cardMesh.material = deathMaterial;
        PopUpTextManager.Instance.ShowScreen("Death Card Screen"); //show death screen overlay
        StartCoroutine(CardRemoveDelay()); //start coroutine to remove card and screen overlay
        Debug.Log("Death card! You lose a finger!");
    }
    private void HandleJokerCardInteraction()
    {
        //cardMesh.material.color = jokerColor; //change card color to joker color
        cardMesh.material = jokerMaterial;
        StartCoroutine(CardRemoveDelay()); //start coroutine to remove card and screen overlay
        Debug.Log("Joker card!");
    }
    void OnMouseEnter()
    {
        if (!isClicked && StateTest.Instance.CurrentEncounterState == StateTest.EncounterState.PlayerTurn)
        {
            cardMesh.material.SetColor("_EmissiveColor", highlightColor); //highlight item
            //cardMesh.material.color = highlightColor;
        }
    }
    void OnMouseExit()
    {
        if (!isClicked && StateTest.Instance.CurrentEncounterState == StateTest.EncounterState.PlayerTurn)
        {
            cardMesh.material.SetColor("_EmissiveColor", unHighlightColor); //unhighlight item
            //cardMesh.material.color = unflippedColor;
        }
    }
    void OnMouseUp()
    {
        if (isClicked == false && StateTest.Instance.CurrentEncounterState == StateTest.EncounterState.PlayerTurn)
        {
            AudioManager.Instance.PlaySound("CardFlip" + Random.Range(1, 3).ToString());
            StateTest.Instance.UpdateEncounterState(StateTest.EncounterState.PlayerSafe);
            isClicked = true;

            if (isSafeCard)
            {
                HandleSafeCardInteraction();
            }
            else if (isJokerCard)
            {
                HandleJokerCardInteraction();
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
            AudioManager.Instance.PlaySound("CardFlip4");
            playerDeck.DrawHand();
        }
        if (PlayerHealthTest.Instance.GetCurrentHealth() > 0)
        {
            StateTest.Instance.UpdateEncounterState(StateTest.EncounterState.EnemyTurn);
        }

    }
    private IEnumerator CardRemoveDelay()
    {
        yield return new WaitForSeconds(cardRemoveDelayTime); //wait for the delay time before removing the card from the table
        PopUpTextManager.Instance.CloseScreen(PopUpTextManager.Instance.currentScreen); //close chosen card screen overlay
        if (PlayerHealthTest.Instance.GetCurrentHealth() <= 0)
        {
            PopUpTextManager.Instance.ShowScreen("Lose Screen");
        }
        Invoke("SwitchState", cardRemoveDelayTime);
    }
}
