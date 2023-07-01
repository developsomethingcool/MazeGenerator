using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Options;
    public GameObject GameoverMenu;
    public GameObject VictoryMenu;

    private void Start()
    {
        if(LoadingSettings.showRespawnMenu == "GameoverMenu")
        {
            ShowGameoverMenu();
        } else if (LoadingSettings.showRespawnMenu == "VictoryMenu")
        {
            ShowVictoryMenu();
        }
        else
        {
            ShowMainMenu();
        }
       
    }

    public void PlayGame()
    {
        Debug.Log("Inside of player menu!!!!!!!!!!!!!!!!!!!");
        SceneManager.LoadScene("Gameplay");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RestartGame()
    {
        Debug.Log("Inside of Restart menu!!!!!!!!!!!!!!!!!!!");
        SceneManager.LoadScene("Gameplay");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowMainMenu()
    {
        DisableAllMenus();
        Debug.Log("Got inside of main menu!");
        LoadingSettings.showRespawnMenu = "";
        MainMenu.SetActive(true);
    }

    public void ShowOptions()
    {
        DisableAllMenus();
        Options.SetActive(true);
    }

    public void ShowGameoverMenu()
    {
        DisableAllMenus();
        GameoverMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void ShowVictoryMenu()
    {
        DisableAllMenus();
        VictoryMenu.SetActive(true);
        
    }

    private void DisableAllMenus()
    {
        MainMenu.SetActive(false);
        Options.SetActive(false);
        GameoverMenu.SetActive(false);
        VictoryMenu.SetActive(false);
    }

}
