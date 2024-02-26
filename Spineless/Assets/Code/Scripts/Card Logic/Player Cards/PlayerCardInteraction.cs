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
    public Color unflippedColor = Color.black;
    private MeshRenderer cardMesh;

    private PlayerDeckLogic playerDeck; // Reference to the playerDeckLogic script
    [SerializeField] private float cardRemoveDelayTime;
    void OnEnable()
    {
        PlayerAnimationTrigger.OnAnimationFinished += SwitchToEnemyTurn;
    }
    void OnDisable()
    {
        PlayerAnimationTrigger.OnAnimationFinished -= SwitchToEnemyTurn;
    }
    void Start()
    {
        isClicked = false;
        // Determine whether the card is safe or death or joker (you can implement your logic here)
        isSafeCard = CheckIfSafeCard();
        isJokerCard = CheckIfJokerCard();

        //Assign card mesh for highlighting
        cardMesh = GetComponent<MeshRenderer>();

        //Start card with unflipped material
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

    void HandleSafeCardInteraction()
    {
        //cardMesh.material.color = safeColor; //change card color to safe color
        cardMesh.material = safeMaterial;  //change card material to safe material
        PopUpTextManager.Instance.ShowScreen("Safe Card Screen"); //show safe screen overlay
        StartRemove(); //start coroutine to remove card and screen overlay
        Debug.Log("Safe card! Your turn ends.");
    }

    void HandleDeathCardInteraction()
    {
        AudioManager.Instance.PlaySound("SeveredHand");
        PlayerHealthTest.Instance.ChangeHealth(-1); //Decrease health
        cardMesh.material = deathMaterial;  //change card material to death material
        PopUpTextManager.Instance.ShowScreen("Death Card Screen"); //show death screen overlay
        StartRemove(); //start coroutine to remove card and screen overlay
        Debug.Log("Death card! You lose a finger!");
    }
    private void HandleJokerCardInteraction()
    {
        cardMesh.material = jokerMaterial; //change card material to joker material
        StartCoroutine(CardRemoveDelay()); //start coroutine to remove card and screen overlay
        Debug.Log("Joker card!");
    }
    void OnMouseEnter()
    {
        if (!isClicked && StateTest.Instance.CurrentEncounterState == StateTest.EncounterState.PlayerTurn)
        {
            cardMesh.material.SetColor("_EmissiveColor", highlightColor); //highlight item
        }
    }
    void OnMouseExit()
    {
        if (!isClicked && StateTest.Instance.CurrentEncounterState == StateTest.EncounterState.PlayerTurn)
        {
            cardMesh.material.SetColor("_EmissiveColor", unHighlightColor); //unhighlight item
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

    private void SwitchToEnemyTurn()
    {
        if (isClicked) //if card has been flipped (for animation finished event)
        {
            if (PlayerHealthTest.Instance.GetCurrentHealth() > 0) //if the player is still alive, switch to enemy turn
            {
                StateTest.Instance.UpdateEncounterState(StateTest.EncounterState.EnemyTurn);

                playerDeck.RemoveCardFromTable(gameObject); //remove the card from the table
                                                                        
                if (playerDeck.CheckTableCards() == 0) //if table is empty of cards
                {
                    //if table is empty after flipping a card, redraw all cards (THIS COULD HAPPEN WHEN PLAYER'S TURN STARTS INSTEAD)
                    AudioManager.Instance.PlaySound("CardFlip4");
                    playerDeck.DrawHand();
                }
            }
        }

    }
    private void StartRemove()
    {
        StartCoroutine("CardRemoveDelay");
    }
    private IEnumerator CardRemoveDelay()
    {
        yield return new WaitForSeconds(cardRemoveDelayTime); //wait for the delay time before removing the card from the table
        if (isSafeCard)
        {
            Invoke("SwitchToEnemyTurn", cardRemoveDelayTime);
        }
    }
}
