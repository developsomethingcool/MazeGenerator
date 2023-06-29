using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject options;
    public GameObject gameoverMenu;

    private void Start()
    {
        if (LoadingSettings.showRespawnMenu)
        {
            ShowGameoverMenu();
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
    }

    public void RestartGame()
    {
        Debug.Log("Inside of player menu!!!!!!!!!!!!!!!!!!!");
        SceneManager.LoadScene("Gameplay");
    }

    public void ShowMainMenu()
    {
        DisableAllMenus();
        mainMenu.SetActive(true);
    }

    public void ShowOptions()
    {
        DisableAllMenus();
        options.SetActive(true);
    }

    public void ShowGameoverMenu()
    {
        DisableAllMenus();
        gameoverMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    private void DisableAllMenus()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        gameoverMenu.SetActive(false);
    }
}
