using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PopUpTextManager : MonoBehaviour
{
    [SerializeField] private ScreenUtil[] screens;
    private static PopUpTextManager _instance;
    public static PopUpTextManager Instance { get { return _instance; } }

    public string currentScreen;

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
    }
    void Start()
    {
        PopUpTextManager.Instance.CloseAllScreens();
    }

    public void ShowScreen(string name)
    {
        ScreenUtil screenCheck = Array.Find(screens, screen => screen.name == name);

        if (screenCheck == null)// check for existence of menu name
        {
            Debug.LogWarning("Screen: " + name + " not found!");
            return;
        }
        if (name == currentScreen)
        { //check if menu is already opened
            Debug.LogWarning("Screen: " + name + " already open!");
            return;
        }
        foreach (ScreenUtil screen in screens) //search list of menus
        {
            if (screen.name == name) //if currently indexed screen name matches, turn it on
            {
                screen.canvas.SetActive(true);
                currentScreen = name; //update current screen to the new screen opened
            }
            else
            {
                screen.canvas.SetActive(false); //if currently indexed menu does not match name, turn it off
            }
        }
        if (name != "Lose Screen" && name != "Win Screen")
        {
            StartCoroutine("ScreenOff");
        }
    }
    public void CloseScreen(string name)
    {
        ScreenUtil screenCheck = Array.Find(screens, screen => screen.name == name);

        if (screenCheck == null)// check for existence of menu name
        {
            Debug.LogWarning("Screen: " + name + " not found!");
            return;
        }
        if (name == currentScreen)
        { //check if menu is already opened
            Debug.LogWarning("Screen: " + name + " already open!");
            return;
        }
        foreach (ScreenUtil screen in screens) //search list of menus
        {
            if (screen.name == name) //if currently indexed screen name matches, turn it on
            {
                screen.canvas.SetActive(false);
                currentScreen = null; //update current screen to the new screen opened
            }
        }
    }
    public void CloseAllScreens()
    {
        foreach (ScreenUtil screen in screens)
        {
            screen.canvas.SetActive(false);
            currentScreen = null;
        }
    }

    private IEnumerator ScreenOff()
    {
        yield return new WaitForSeconds(0.5f);
        CloseAllScreens();
        if (StateTest.Instance.CurrentEncounterState == StateTest.EncounterState.EnemyTurn)
        {
            StateTest.Instance.UpdateEncounterState(StateTest.EncounterState.PlayerTurn);
        }
    }
}
