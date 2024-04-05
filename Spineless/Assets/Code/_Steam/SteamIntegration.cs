using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamIntegration : MonoBehaviour
{
    private static SteamIntegration _instance;
    public static SteamIntegration Instance { get { return _instance; } } //to use any method from this manager call MenuManager.Instance."FunctionName"(); anywhere in any script
    void Awake()
    {
        //on awake check for existence of manager and handle accordingly
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        try
        {
            Steamworks.SteamClient.Init(2866710);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }

        //Steamworks.SteamUserStats.ResetAll(true); //FOR TESTING
    }

    void Update()
    {
        Steamworks.SteamClient.RunCallbacks();
    }

    void OnApplicationQuit()
    {
        Steamworks.SteamClient.Shutdown();
    }
    public static void UnlockAchievement(string achievementName)
    {
        var ach = new Steamworks.Data.Achievement();
        switch (achievementName)
        {
            case "NormalWin":
                ach = new Steamworks.Data.Achievement("WIN_ACHIEVEMENT_NORMAL");
                if (ach.State == false)
                {
                    ach.Trigger();
                }
                break;
            case "HardWin":
                ach = new Steamworks.Data.Achievement("WIN_ACHIEVEMENT_HARD");
                if (ach.State == false)
                {
                    ach.Trigger();
                }
                break;
            case "ShopVisited":
                ach = new Steamworks.Data.Achievement("SHOP_VISITED_ACHIEVEMENT");
                if (ach.State == false)
                {
                    ach.Trigger();
                }
                break;
            case "FirstItem":
                ach = new Steamworks.Data.Achievement("FIRST_ITEM_ACHIEVEMENT");
                if (ach.State == false)
                {
                    ach.Trigger();
                }
                break;
            case "LoseGame":
                ach = new Steamworks.Data.Achievement("LOSE_ACHIEVEMENT");
                if (ach.State == false)
                {
                    ach.Trigger();
                }
                break;
            case "PocketKnife":
                ach = new Steamworks.Data.Achievement("POCKET_KNIFE_ACHIEVEMENT");
                if (ach.State == false)
                {
                    ach.Trigger();
                }
                break;
            case "Camera":
                ach = new Steamworks.Data.Achievement("CAMERA_ACHIEVEMENT");
                if (ach.State == false)
                {
                    ach.Trigger();
                }
                break;
            case "Eye":
                ach = new Steamworks.Data.Achievement("EYE_ACHIEVEMENT");
                if (ach.State == false)
                {
                    ach.Trigger();
                }
                break;
            case "Snake":
                ach = new Steamworks.Data.Achievement("SNAKE_ACHIEVEMENT");
                if (ach.State == false)
                {
                    ach.Trigger();
                }
                break;
            case "Jack":
                ach = new Steamworks.Data.Achievement("JACK_ACHIEVEMENT");
                if (ach.State == false)
                {
                    ach.Trigger();
                }
                break;
            case "HolyCross":
                ach = new Steamworks.Data.Achievement("HOLY_CROSS_ACHIEVEMENT");
                if (ach.State == false)
                {
                    ach.Trigger();
                }
                break;
            case "Matchbox":
                ach = new Steamworks.Data.Achievement("MATCHBOX_ACHIEVEMENT");
                if (ach.State == false)
                {
                    ach.Trigger();
                }
                break;
            case "SeveredHand":
                ach = new Steamworks.Data.Achievement("SEVERED_HAND_ACHIEVEMENT");
                if (ach.State == false)
                {
                    ach.Trigger();
                }
                break;
            case "Stapler":
                ach = new Steamworks.Data.Achievement("STAPLER_ACHIEVEMENT");
                if (ach.State == false)
                {
                    ach.Trigger();
                }
                break;
            case "Wishbone":
                ach = new Steamworks.Data.Achievement("WISHBONE_ACHIEVEMENT");
                if (ach.State == false)
                {
                    ach.Trigger();
                }
                break;
            case "Joker":
                ach = new Steamworks.Data.Achievement("JOKER_ACHIEVEMENT");
                if (ach.State == false)
                {
                    ach.Trigger();
                }
                break;
            case "FirstEncounter":
                ach = new Steamworks.Data.Achievement("ENCOUNTER_ACHIEVEMENT");
                if (ach.State == false)
                {
                    ach.Trigger();
                }
                break;
            default:
                break;
        }
    }
}
