using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterRoomStartup : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerDeckLogic playerDeck;
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private GameObject dotCanvas;
    [SerializeField] private AudioClip tutorial2AudioClip;
    private bool encounterStarted;
    private int entryHP;
    void Awake()
    {
        //play tutorial if this is their first time in an encounter
        if (saveData.FirstEncounterEntered == false)
        {
            encounterStarted = false;
            playerDeck.totalDeathCards = 4;
            tutorialCanvas.SetActive(true);
            dotCanvas.SetActive(false);

            StartCoroutine("TutorialDelay");
            //play tutorial
        }
        else
        {
            encounterStarted = true;
        }
    }
    void Start()
    {
        if (saveData.FirstEncounterEntered == false)
        {
            AudioManager.Instance.PlaySound("Tutorial2");
            saveData.FirstEncounterEntered = true;
        }

        AudioManager.Instance.PlayMusicTrack("Encounter Music"); //Play track for encounter
        saveData.ShopVisited = false; //every time player enters encounter room, reset shop availability

        //Set character's starting animation to proper finger count
        entryHP = saveData.playerFingersInNextEncounter;
        if (entryHP == 3)
        {
            playerAnimator.SetTrigger("3 HP Entry");
        }
        else if (entryHP == 1)
        {
            playerAnimator.SetTrigger("1 HP Entry");
        }
    }

    private IEnumerator TutorialDelay()
    {
        yield return new WaitForSecondsRealtime(107);
        tutorialCanvas.SetActive(false);
        dotCanvas.SetActive(true);
    }
}
