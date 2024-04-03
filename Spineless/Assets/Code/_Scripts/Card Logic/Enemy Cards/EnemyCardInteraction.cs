using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class EnemyCardInteraction : MonoBehaviour
{
    private bool isSafeCard;
    private bool isDeathCard;
    private bool isJokerCard;
    public Color highlightColor = new Color(1f, 1f, 1f, 0.5f); // White with 50% opacity;
    public Color unHighlightColor = Color.black;
    public Material cardBackMaterial;
    public Material safeMaterial;
    public Material deathMaterial;
    public Material jokerMaterial;
    public Color safeColor = Color.green;
    public Color deathColor = Color.red;
    public Color unflippedColor = Color.black;
    [HideInInspector] public MeshRenderer CardMesh;
    [HideInInspector] public bool isClicked;
    [HideInInspector] public bool isSelected;
    [SerializeField] private GameObject cardHolderObject;
    [SerializeField] private Animator cardAnimator;

    private EnemyDeckLogic enemyDeck; // Reference to the enemyDeckLogic script
    [SerializeField] private float cardRemoveDelayTime;
    [SerializeField] private float jokerDelayTime;
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private EncounterData encounterData;

    public delegate void JokerExecutionCompleted2();
    public static event JokerExecutionCompleted2 OnJokerExecutionCompleted2;

    public delegate void EnemyTurnFinished();
    public static event EnemyTurnFinished OnEnemyTurnFinished;
    void OnEnable()
    {
        EnemyAnimationTrigger.OnEnemyAnimationFinished += SwitchToPlayerTurn;
    }
    void OnDisable()
    {
        EnemyAnimationTrigger.OnEnemyAnimationFinished -= SwitchToPlayerTurn;
    }
    void Start()
    {
        isSelected = false;
        isClicked = false;
        // Determine whether the card is safe or death (you can implement your logic here)
        isSafeCard = CheckIfSafeCard();
        isDeathCard = CheckIfDeathCard();
        isJokerCard = CheckIfJokerCard();

        //Assign card mesh for highlighting
        CardMesh = GetComponentInChildren<MeshRenderer>();

        //Start card with unflipped color
        CardMesh.material = cardBackMaterial;


        // Find the enemyDeckLogic script in the scene
        enemyDeck = FindObjectOfType<EnemyDeckLogic>();

        // If the script is not found, log an error
        if (enemyDeck == null)
        {
            Debug.LogError("enemyDeckLogic script not found in the scene. Make sure it's present in the scene.");
        }
    }

    void OnMouseEnter() //used to interact with enemy cards in joker execution state
    {
        if (!PauseManager.Instance.IsPaused())
        {
            if (!isClicked && StateManager.Instance.CurrentEncounterState == StateManager.EncounterState.PlayerJokerExecution)
            {
                //highlight all enemy death cards
                for (int i = 0; i < encounterData.EnemyTableCards.Count; i++)
                {
                    encounterData.EnemyTableCards[i].GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissiveColor", highlightColor); //highlight currently indexed card
                }
            }
            else if (!isClicked && StateManager.Instance.CurrentEncounterState == StateManager.EncounterState.PlayerKnife)
            {
                CardMesh.material.SetColor("_EmissiveColor", highlightColor);
            }
        }

    }
    void OnMouseExit() //used to interact with enemy cards in joker execution state
    {
        if (!PauseManager.Instance.IsPaused())
        {
            if (!isClicked && StateManager.Instance.CurrentEncounterState == StateManager.EncounterState.PlayerJokerExecution)
            {
                //unhighlight all enemy table cards
                for (int i = 0; i < encounterData.EnemyTableCards.Count; i++)
                {
                    encounterData.EnemyTableCards[i].GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissiveColor", unHighlightColor); //highlight currently indexed card
                }
            }
            else if (!isClicked && StateManager.Instance.CurrentEncounterState == StateManager.EncounterState.PlayerKnife)
            {
                CardMesh.material.SetColor("_EmissiveColor", unHighlightColor);
            }
        }

    }
    void OnMouseUp()
    {
        if (!PauseManager.Instance.IsPaused())
        {
            if (isClicked == false && StateManager.Instance.CurrentEncounterState == StateManager.EncounterState.PlayerJokerExecution)
            {
                isClicked = true;

                for (int i = 0; i < encounterData.PlayerTableCards.Count; i++)//set all player table cards isCLicked to true to avoid multiple joker executions on different cards
                {
                    encounterData.PlayerTableCards[i].GetComponentInChildren<PlayerCardInteraction>().isClicked = true;
                }
                for (int i = 0; i < encounterData.EnemyTableCards.Count; i++)//set all enemy table cards isCLicked to true to avoid multiple joker executions on different cards
                {
                    encounterData.EnemyTableCards[i].GetComponentInChildren<EnemyCardInteraction>().isClicked = true;
                }
                ExecutePlayerJoker();
            }
            else if (!isClicked && StateManager.Instance.CurrentEncounterState == StateManager.EncounterState.PlayerKnife)
            {
                CardMesh.material.SetColor("_EmissiveColor", unHighlightColor);
                ShowCard();
                StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.PlayerTurn);
            }
        }

    }

    public bool CheckIfSafeCard()
    {
        // For simplicity, let's say safe cards have the tag "SafeCard" and death cards have the tag "DeathCard"
        return gameObject.CompareTag("SafeCard");
    }
    public bool CheckIfDeathCard()
    {
        return gameObject.CompareTag("DeathCard");
    }
    public bool CheckIfJokerCard()
    {
        return gameObject.CompareTag("JokerCard");
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
        //cardAnimator.SetTrigger("Flip");
    }

    void HandleSafeCardInteraction()
    {
        //CardMesh.material = safeMaterial;
        //PopUpTextManager.Instance.ShowScreen("Safe Card Screen"); //show safe screen 
        StartCoroutine(CardRemoveDelay());
        Debug.Log("Enemy safe card!");
    }

    void HandleDeathCardInteraction()
    {
        //Chopping finger animation goes here
        AudioManager.Instance.PlaySound("SeveredHand");
        if (saveData.monsterFingers < 10)
        {
            saveData.monsterFingers++; //increase monster fingers player currency
        }
        //CardMesh.material = deathMaterial;
        //PopUpTextManager.Instance.ShowScreen("Death Card Screen"); //show death screen 
        StartCoroutine(CardRemoveDelay());
        Debug.Log("Enemy Death Card!");
    }
    public void EnemyCardSelected()
    {
        cardAnimator.SetTrigger("Flip");
        isSelected = true;
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
    private void SwitchToPlayerTurn()
    {
        if (isSelected) //if card has been selected (to not include non-selected cards for removal when animation finished event broadcasts)
        {
            if (EnemyHealthTest.Instance.GetCurrentHealth() > 0) //if the enemy is still alive, switch to enemy turn
            {
                if (OnEnemyTurnFinished != null)
                {
                    OnEnemyTurnFinished?.Invoke();
                }
                StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.PlayerTurn);

                enemyDeck.RemoveCardFromTable(cardHolderObject); //remove the card from the table
            }
        }
    }

    private IEnumerator CardRemoveDelay()
    {
        yield return new WaitForSeconds(cardRemoveDelayTime); //wait for the delay time before removing the card from the table
        if (isSafeCard)
        {
            Debug.Log("In Enemy card Interaction Safe Card Exit");
            Invoke("SwitchToPlayerTurn", cardRemoveDelayTime);
        }
        if (isJokerCard)
        {
            if (encounterData.JokerCardsCollected != 3)
            {
                Invoke("SwitchToPlayerTurn", cardRemoveDelayTime);
            }
        }
        if (isDeathCard)
        {
            EnemyHealthTest.Instance.ChangeHealth(-1); //Decrease health
        }

    }

    private void ExecutePlayerJoker()
    {

        //check enemy death card count
        int enemyDeathCards = 0;
        for (int i = 0; i < encounterData.EnemyTableCards.Count; i++)
        {
            if (encounterData.EnemyTableCards[i].name.Contains("Death")) //if currently indexed card is a death card, increment death card count
            {
                enemyDeathCards++;
            }
        }

        //if enemy has no death cards
        if (enemyDeathCards == 0)
        {
            //highlight all enemy table cards with red
            for (int i = 0; i < encounterData.EnemyTableCards.Count; i++)
            {
                encounterData.EnemyTableCards[i].GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissiveColor", deathColor);
            }

            StartCoroutine("JokerFailExecutionDelay");
        }
        //if enemy has at least one death card
        else if (enemyDeathCards > 0)
        {
            //highlight the death cards with green
            for (int i = 0; i < encounterData.EnemyTableCards.Count; i++)
            {
                if (encounterData.EnemyTableCards[i].name.Contains("Death"))
                {
                    encounterData.EnemyTableCards[i].GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissiveColor", safeColor);
                }
            }
            //start delay for joker safe execution
            StartCoroutine("JokerSafeExecutionDelay");
        }
    }

    private IEnumerator JokerSafeExecutionDelay()  //delay for if the player successfully executes joker on enemy (enemy has death cards)
    {
        yield return new WaitForSeconds(jokerDelayTime); //delay removal of cards

        //remove death cards
        for (int i = 0; i < encounterData.EnemyTableCards.Count; i++)
        {
            if (encounterData.EnemyTableCards[i].name.Contains("Death"))
            {
                enemyDeck.RemoveCardFromTable(encounterData.EnemyTableCards[i]);
            }
        }
        //revert color for remaining cards on table
        for (int i = 0; i < encounterData.EnemyTableCards.Count; i++)
        {
            encounterData.EnemyTableCards[i].GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissiveColor", unHighlightColor);
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
        if (OnJokerExecutionCompleted2 != null)
        {
            OnJokerExecutionCompleted2?.Invoke();
        }

        //switch to enemy turn
        StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.EnemyTurn);
    }

    private IEnumerator JokerFailExecutionDelay()  //delay for if the player joker execution on enemy fails (enemy did not have death cards)
    {
        yield return new WaitForSeconds(jokerDelayTime); //delay effect

        //revert color for remaining cards on table
        for (int i = 0; i < encounterData.EnemyTableCards.Count; i++)
        {
            encounterData.EnemyTableCards[i].GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissiveColor", unHighlightColor);
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

        //remove a finger
        EnemyHealthTest.Instance.ChangeHealth(-1);

        //if the monster is still alive in the encounter
        if (EnemyHealthTest.Instance.GetCurrentHealth() > 0)
        {
            //broadcast that joker execution has finished
            if (OnJokerExecutionCompleted2 != null)
            {
                OnJokerExecutionCompleted2?.Invoke();
            }
        }
        //otherwise, the monster is dead and the enemy health system will execute the win condition logic
        //switch to enemy turn
        StateManager.Instance.UpdateEncounterState(StateManager.EncounterState.EnemyTurn);
    }
}
