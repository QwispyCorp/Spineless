using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script that turns off/on death card count depending on whether special item ability is currently active or not 
public class ItemPromptController : MonoBehaviour
{
    [SerializeField] private GameObject ekgCanvas;
    [SerializeField] private GameObject deathCardText;
    [SerializeField] private GameObject eyePrompt;
    [SerializeField] private GameObject knifePrompt;
    [SerializeField] private GameObject jokerPrompt;


    void OnEnable()
    {
        ItemMouseInteraction.OnEyeUsed += ShowEyePrompt;  //receive event that eye has been used
        ItemMouseInteraction.OnKnifeUsed += ShowKnifePrompt; //receive event that pocket knife has been used
        JokerCardCollector.OnJokerStart += ShowJokerPrompt; //receive event that joker execution has started

        PlayerCardInteraction.OnEyeExecuted += HideEyePrompt; //receive event that eye card has been executed
        EnemyCardInteraction.OnKnifeExecuted += HideKnifePrompt; //receive event that pocket knife card has been executed
        PlayerCardInteraction.OnJokerExecutionCompleted += HideJokerPrompt; //receive event that joker execution has finished
        EnemyCardInteraction.OnJokerExecutionCompleted2 += HideJokerPrompt; //receive event that joker execution has finished
    }
    void OnDisable()
    {
        ItemMouseInteraction.OnEyeUsed -= ShowEyePrompt;
        ItemMouseInteraction.OnKnifeUsed -= ShowKnifePrompt;
        JokerCardCollector.OnJokerStart -= ShowJokerPrompt;

        PlayerCardInteraction.OnEyeExecuted -= HideEyePrompt;
        EnemyCardInteraction.OnKnifeExecuted -= HideKnifePrompt;
        PlayerCardInteraction.OnJokerExecutionCompleted -= HideJokerPrompt;
        EnemyCardInteraction.OnJokerExecutionCompleted2 -= HideJokerPrompt;
    }

    //--------------------------- SHOW PROMPTS
    void ShowKnifePrompt()
    {
        //spawn pocket knife prompt text
        knifePrompt.SetActive(true);
        //turn off death card count
        deathCardText.SetActive(false);
    }
    void ShowEyePrompt()
    {
        //spawn eye prompt text
        eyePrompt.SetActive(true);
        //turn off death card count
        deathCardText.SetActive(false);
    }

    void ShowJokerPrompt()
    {
        //spawn joker prompt
        jokerPrompt.SetActive(true);
        //turn off death card count
        deathCardText.SetActive(false);
    }

    //--------------------------- HIDE PROMPTS
    void HideKnifePrompt()
    {
        //destroy pocket knife prompt text
        knifePrompt.SetActive(false);
        //turn on death card count
        deathCardText.SetActive(true);
    }
    void HideEyePrompt()
    {
        //destroy eye prompt text
        eyePrompt.SetActive(false);
        //turn on death card count
        deathCardText.SetActive(true);
    }

    void HideJokerPrompt()
    {
        //destroy joker prompt
        jokerPrompt.SetActive(false);
        //turn on death card count
        deathCardText.SetActive(true);
    }





}
