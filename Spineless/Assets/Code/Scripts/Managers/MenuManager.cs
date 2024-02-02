using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//A menu manager that works by storing menu canvases as game objects and turning them off/on depending on which menu the player is currently on.  

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Menu[] menus;

    //local and global manager instances
    private static MenuManager _instance;
    public static MenuManager Instance { get { return _instance; } } //to use any method from this manager call MenuManager.Instance."FunctionName"(); anywhere in any script

    private string currentMenu;
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

        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") //on start, if the player is in the main menu scene, open the main menu
        {
            Instance.OpenMenu("Main Menu");
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        { //The input to return to main menu will be ESC, input currently checks for "Backspace" key instead of ESC for testing in game mode
            if (currentMenu == "Options Menu" || currentMenu == "Credits Menu")
            {
                Instance.OpenMenu("Main Menu");
            }
        }
    }

    /* -------------------------------------------------------------------------- */
    /*                             MENU MANAGER METHODS                           */
    /* -------------------------------------------------------------------------- */

    //OpenMenu method opens any meny by passing its name given in inspector and closes all other menus. Usage outside of this class: MenuManager.Instance.OpenMenu("Menu Name");
    public void OpenMenu(string name)
    {
        Menu menuCheck = Array.Find(menus, menu => menu.name == name);

        if (menuCheck == null)// check for existence of menu name
        {
            Debug.LogWarning("Menu: " + name + " not found!");
            return;
        }
        if (name == currentMenu)
        { //check if menu is already opened
            Debug.LogWarning("Menu: " + name + " already open!");
            return;
        }
        foreach (Menu menu in menus) //search list of menus
        {
            if (menu.name == name) //if currently indexed menu name matches, turn it on
            {
                menu.canvas.SetActive(true);
                currentMenu = name; //update current menu to the new menu opened
            }
            else
            {
                menu.canvas.SetActive(false); //if currently indexed menu does not match name, turn it off
            }
        }
    }
    //CloseMenu method closes any meny by passing its name given in inspector and closes all other menus. Usage outside of this class: MenuManager.Instance.OpenMenu("Menu Name");
    public void CloseMenu(string name)
    {
        Menu menuCheck = Array.Find(menus, menu => menu.name == name);

        if (menuCheck == null)// check for existence of menu name
        {
            Debug.LogWarning("Menu: " + name + " not found!");
            return;
        }
        if (name == currentMenu)
        { //check if menu is already opened
            Debug.LogWarning("Menu: " + name + " already open!");
            return;
        }
        foreach (Menu menu in menus) //search list of menus
        {
            if (menu.name == name) //if currently indexed menu name matches, turn it on
            {
                menu.canvas.SetActive(false);
                currentMenu = name; //update current menu to the new menu opened
            }
        }
    }

    //CloseAllMenus closes all menus that may be open. Usage outside of this class: MenuManager.Instance.CloseAllMenus();
    public void CloseAllMenus()
    {
        foreach (Menu menu in menus)
        {
            menu.canvas.SetActive(false);
            currentMenu = null;
        }
    }

    /* -------------------------------------------------------------------------- */
    /*                               BUTTON METHODS                               */
    /* -------------------------------------------------------------------------- */
    /*                    methods used for OnClick button events                  */
    /* -------------------------------------------------------------------------- */
    public void PlayGame()
    {
        Instance.CloseAllMenus();
        SceneManager.LoadScene("GameBoard");
        gameObject.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Instance.OpenMenu("Main Menu");
    }
    public void GoToOptionsMenu()
    {
        Instance.OpenMenu("Options Menu");
    }
    public void GoToCreditsMenu()
    {
        Instance.OpenMenu("Credits Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
