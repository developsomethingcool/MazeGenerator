using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //All for ui screens
    public GameObject mainMenu;
    public GameObject options;
    public GameObject gameoverMenu;
    public GameObject victoryMenu;

    //text to output a final time
    public TextMeshProUGUI textMeshProUI;

    private void Start()
    {
        //in case if scene Menu defined as gameoverMenu, then load GameOverMenu
        //in case if Menu defined as victoryMenu, then load victoryMenu
        //otherwise load MainMenu
        if (LoadingSettings.showRespawnMenu == "gameoverMenu")
        {
            ShowGameoverMenu();
        } else if (LoadingSettings.showRespawnMenu == "victoryMenu")
        {
            ShowVictoryMenu();
        }
        else
        {
            ShowMainMenu();
        }

        textMeshProUI = GetComponent<TextMeshProUGUI>();
    }

    // start the Gameplay
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
        //make mouse cursor invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //restart the game
    public void RestartGame()
    {   
        SceneManager.LoadScene("Gameplay");
        //make mouse cursor invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //shows the main menu by enabling the mainMenu game object and disabling other menus.
    public void ShowMainMenu()
    {
        DisableAllMenus();
        mainMenu.SetActive(true);
    }

    //shows the options menu by enabling the options game object and disabling other menus.
    public void ShowOptions()
    {
        DisableAllMenus();
        options.SetActive(true);
    }

    // shows the game over menu by enabling the gameoverMenu game object and disabling other menus.
    public void ShowGameoverMenu()
    {
        DisableAllMenus();
        gameoverMenu.SetActive(true);
    }

    //quit the game
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    //shows the victory menu by enabling the victoryMenu game object and disabling other menus
    public void ShowVictoryMenu()
    {
        DisableAllMenus();
        string time = FindObjectOfType<Data_Percistence>().getEndTime();
        textMeshProUI.text = time;
        victoryMenu.SetActive(true);

    }

    //method that disables all menu game objects
    private void DisableAllMenus()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        gameoverMenu.SetActive(false);
        victoryMenu.SetActive(false);
    }

}
