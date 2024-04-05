using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterRoomStartup : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private GameDifficulty difficulty;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerDeckLogic playerDeck;
    [SerializeField] private GameObject dotCanvas;
    private int entryHP;
    void Awake()
    {
        //play tutorial if this is their first time in an encounter
        if (saveData.FirstEncounterEntered == false)
        {
            if (difficulty.NormalMode)
            {
                playerDeck.totalDeathCards = 4;
            }
        }
    }
    void Start()
    {
        if (saveData.FirstEncounterEntered == false)
        {
            saveData.FirstEncounterEntered = true;
            SteamIntegration.UnlockAchievement("FirstEncounter");
        }

        AudioManager.Instance.PlayMusicTrack("Encounter Music"); //Play track for encounter

        AudioManager.Instance.StopAllSounds();
        AudioManager.Instance.MuffleMusic();

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
}
