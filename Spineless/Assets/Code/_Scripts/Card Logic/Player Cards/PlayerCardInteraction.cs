using UnityEngine;
using System.Collections;
using System;
using Unity.VisualScripting;

public class PlayerCardInteraction : MonoBehaviour
{
    private bool isSafeCard;
    [HideInInspector] public bool isJokerCard;
    private bool isDeathCard;
    [HideInInspector] public bool isClicked;
    public Color highlightColor = new Color(1f, 1f, 1f, 0.5f); // White with 50% opacity;
    public Color unHighlightColor = Color.black; // White with 50% opacity;
    public Color safeColor = Color.green;
    public Color deathColor = Color.red;
    public Material cardBackMaterial;
    public Material safeMaterial;
    public Material deathMaterial;
    public Material jokerMaterial;
    public Color unflippedColor = Color.black;
    [HideInInspector] public MeshRenderer CardMesh;
    [HideInInspector] public Animator CardAnimator;
    [SerializeField] private GameObject CardHolderObject;

    private PlayerDeckLogic playerDeck; // Reference to the playerDeckLogic script
    [SerializeField] private float cardRemoveDelayTime;
    [SerializeField] private float jokerDelayTime;
    [SerializeField] private EncounterData encounterData;
    public delegate void JokerFlipped();
    public static event JokerFlipped OnJokerFlipped;
    public delegate void JokerExecutionCompleted();
    public static event JokerExecutionCompleted OnJokerExecutionCompleted;
    public delegate void EyeExecuted();
    public static event EyeExecuted OnEyeExecuted;

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
        isDeathCard = CheckIfDeathCard();

        //Assign card mesh for highlighting
        CardMesh = GetComponent<MeshRenderer>();

        CardAnimator = GetComponentInParent<Animator>();

        //Start card with unflipped material
        CardMesh.material = cardBackMaterial;

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
    bool CheckIfDeathCard()
    {
        return gameObject.CompareTag("DeathCard");
    }
    public void ShowCard()
    {
        if (isSafeCard)
        {
            CardMesh.material = safeMaterial;
        }
        else if (isJokerCard)
        {
            CardMesh.material = jokerMaterial;
        }
        else
        {
            CardMesh.material = deathMaterial;
        }
        //CardAnimator.SetTrigger("Flip");
    }

    void HandleSafeCardInteraction()
    {
        //CardMesh.material = safeMaterial;  //change card material to safe material
        //PopUpTextManager.Instance.ShowScreen("Safe Card Screen"); //show safe screen overlay
        StartRemove(); //start coroutine to remove card and screen overlay
        Debug.Log("Safe card! Your turn ends.");
    }

    void HandleDeathCardInteraction()
    {
        AudioManager.Instance.PlaySound("SeveredHand");
        //CardMesh.material = deathMaterial;  //change card material to death material
        //PopUpTextManager.Instance.ShowScreen("Death Card Screen"); //show death screen overlay
        StartRemove(); //start coroutine to remove card and screen overlay
        Debug.Log("Death card! You lose a finger!");
    }
    private void HandleJokerCardInteraction()
    {
        StartRemove(); //start coroutine to remove card
        Debug.Log("Joker card!");
    }
    void OnMouseEnter()
    {
        if (!PauseManager.Instance.IsPaused()) //if game is not paused
        {
            if (!isClicked && StateManager.Instance.CurrentEncounterState == StateManager.EncounterState.PlayerTurn)
            {
                CardMesh.material.SetColor("_EmissiveColor", highlightColor); //highlight card
            }
            if (!isClicked && StateManager.Instance.CurrentEncounterState == StateManager.EncounterState.PlayerJokerExecution)
            {
                //highlight all player table cards
                for (int i = 0; i < encounterData.PlayerTableCards.Count; i++)
                {
                    encounterData.PlayerTableCards[i].GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissiveColor", highlightColor); //highlight currently indexed card
                }
            }
            if (!isClicked && StateManager.Instance.CurrentEncounterState == StateManager.EncounterState.PlayerEye)
            {
                GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissiveColor", highlightColor); //highlight card when hovering for eye item use
            }
        }

    }
    void OnMouseExit()
    {
        if (!PauseManager.Instance.IsPaused())
        {
            if (!isClicked && StateManager.Instance.CurrentEncounterState == StateManager.EncounterState.PlayerTurn)
            {
                CardMesh.material.SetColor("_EmissiveColor", unHighlightColor); //unhighlight card
            }
            else if (!isClicked && StateManager.Instance.CurrentEncounterState == StateManager.EncounterState.PlayerJokerExecution)
            {
                //unhighlight all player table cards
                for (int i = 0; i < encounterData.PlayerTableCards.Count; i++)
                {
                    encounterData.PlayerTableCards[i].GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissiveColor", unHighlightColor); //highlight currently indexed card
                }
            }
            else if (!isClicked && StateManager.Instance.CurrentEncounterState == StateManager.EncounterState.PlayerEye)
            {
                CardMesh.material.SetColor("_EmissiveColor", unHighlightColor);
            }
        }

    }
    void OnMouseUp()
    {
        if (!PauseManager.Instance.IsPaused())
        {
            if (!isClicked && StateManager.Instance.CurrentEncounterState == StateManager.EncounterState.PlayerTurn)
            {
                AudioManager.Instance.PlaySound("CardFlip" + UnityEngine.Random.Range(1, 3).ToString());
                CardMesh.material.SetColor("_EmissiveColor", unHighlightColor); //unhighlight card
                isClicked = true;

                if (isSafeCard)
                {
                    CardAnimator.SetTrigger("Flip");
                    StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.PlayerSafe);
                    HandleSafeCardInteraction();
                }
                else if (isJokerCard)
                {
                    StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.PlayerSafe);
                    switch (encounterData.JokerCardsCollected)
                    {
                        case 0:
                            CardAnimator.SetTrigger("Flip");
                            break;
                        case 1:
                            CardAnimator.SetTrigger("Flip2");
                            break;
                        case 2:
                            CardAnimator.SetTrigger("Flip3");
                            break;
                        default:
                            break;
                    }
                    HandleJokerCardInteraction();
                }
                else
                {
                    StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.PlayerDamage);
                    CardAnimator.SetTrigger("Flip");
                    HandleDeathCardInteraction();
                }
            }
            else if (!isClicked && StateManager.Instance.CurrentEncounterState == StateManager.EncounterState.PlayerJokerExecution)
            {
                for (int i = 0; i < encounterData.PlayerTableCards.Count; i++)//set all player table cards isCLicked to true to avoid multiple joker executions on different cards
                {
                    encounterData.PlayerTableCards[i].GetComponentInChildren<PlayerCardInteraction>().isClicked = true;
                }
                for (int i = 0; i < encounterData.EnemyTableCards.Count; i++)//set all enemy table cards isCLicked to true to avoid multiple joker executions on different cards
                {
                    encounterData.EnemyTableCards[i].GetComponentInChildren<EnemyCardInteraction>().isClicked = true;
                }
                //run Joker execution on player table cards:
                ExecutePlayerJoker();
            }

            else if (!isClicked && StateManager.Instance.CurrentEncounterState == StateManager.EncounterState.PlayerEye)
            {
                CardMesh.material.SetColor("_EmissiveColor", unHighlightColor);
                ShowCard();
                if (OnEyeExecuted != null)
                {
                    OnEyeExecuted?.Invoke();
                }
                StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.PlayerTurn);
            }
        }


    }

    private void SwitchToEnemyTurn()
    {
        if (isClicked) //if card has been flipped (for animation finished event)
        {
            if (PlayerHealthTest.Instance.GetCurrentHealth() > 0) //if the player is still alive, switch to enemy turn
            {
                StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.EnemyTurn);
                playerDeck.RemoveCardFromTable(CardHolderObject); //remove the card from the table
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
            Invoke("SwitchToEnemyTurn", cardRemoveDelayTime); //if is safe card, 
        }
        if (isJokerCard)
        {
            //yield return new WaitForSeconds(0.5f);
            yield return new WaitForSeconds(1);
            if (OnJokerFlipped != null)
            {
                OnJokerFlipped?.Invoke(); //broadcast that a joker card has been flipped
            }
            playerDeck.RemoveCardFromTable(CardHolderObject);//if it's a joker card, remove immediately after the delay ends to allow joker execution state
            if (encounterData.JokerCardsCollected != 3)
            {
                StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.EnemyTurn);
            }
        }
        if (isDeathCard)
        {
            PlayerHealthTest.Instance.ChangeHealth(-1); //Decrease health
        }
    }

    private void ExecutePlayerJoker()
    {

        //check player death card count
        int playerDeathCards = 0;
        for (int i = 0; i < encounterData.PlayerTableCards.Count; i++)
        {
            if (encounterData.PlayerTableCards[i].name.Contains("Death")) //if currently indexed card is a death card, increment death card count
            {
                playerDeathCards++;
            }
        }

        //if player has no death cards
        if (playerDeathCards == 0)
        {
            //highlight all player table cards with red
            for (int i = 0; i < encounterData.PlayerTableCards.Count; i++)
            {
                encounterData.PlayerTableCards[i].GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissiveColor", deathColor);
            }

            StartCoroutine("JokerFailExecutionDelay");
        }
        //if player has at least one death card
        else if (playerDeathCards > 0)
        {
            //highlight the death cards with green
            for (int i = 0; i < encounterData.PlayerTableCards.Count; i++)
            {
                if (encounterData.PlayerTableCards[i].name.Contains("Death"))
                {
                    encounterData.PlayerTableCards[i].GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissiveColor", safeColor);
                }
            }
            //start delay for joker safe execution
            StartCoroutine("JokerSafeExecutionDelay");
        }
    }

    private IEnumerator JokerSafeExecutionDelay()  //delay for if the player chose successfully executes joker on themselves
    {
        yield return new WaitForSeconds(jokerDelayTime); //delay removal of cards

        //remove death cards
        for (int i = 0; i < encounterData.PlayerTableCards.Count; i++)
        {
            if (encounterData.PlayerTableCards[i].name.Contains("Death"))
            {
                playerDeck.RemoveCardFromTable(encounterData.PlayerTableCards[i]);
            }
        }
        //revert color for remaining cards on table
        for (int i = 0; i < encounterData.PlayerTableCards.Count; i++)
        {
            encounterData.PlayerTableCards[i].GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissiveColor", unHighlightColor);
        }
        //revert isClicked to false for remaing player cards on table
        for (int i = 0; i < encounterData.PlayerTableCards.Count; i++)
        {
            encounterData.PlayerTableCards[i].GetComponentInChildren<PlayerCardInteraction>().isClicked = false;
        }
        //revert isClicked to false for remaing enemy cards on table
        for (int i = 0; i < encounterData.EnemyTableCards.Count; i++)
        {
            encounterData.EnemyTableCards[i].GetComponentInChildren<EnemyCardInteraction>().isClicked = false;
        }

        //broadcast that joker execution has finished
        if (OnJokerExecutionCompleted != null)
        {
            OnJokerExecutionCompleted?.Invoke();
        }

        //switch to enemy turn
        StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.EnemyTurn);
    }

    private IEnumerator JokerFailExecutionDelay()  //delay for if the player joker execution on themselves fails
    {
        yield return new WaitForSeconds(jokerDelayTime); //delay effect

        //remove a finger
        PlayerHealthTest.Instance.ChangeHealth(-1);

        yield return new WaitForSeconds(PlayerAnimationTrigger.CurrentChopAnimLength);

        //if the player is still alive in the encounter
        if (PlayerHealthTest.Instance.GetCurrentHealth() > 0)
        {
            //broadcast that joker execution has finished
            if (OnJokerExecutionCompleted != null)
            {
                OnJokerExecutionCompleted?.Invoke();
            }
            //switch to enemy turn
            StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.EnemyTurn);
        }
        //otherwise, the player is dead and the player health system will execute the lose condition logic
    }

}
