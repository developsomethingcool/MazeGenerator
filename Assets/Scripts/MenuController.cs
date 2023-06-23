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
            ShowMainMenu();
            //ShowGameoverMenu();
        }
        else
        {
            ShowMainMenu();
        }    
    }

    public void PlayGame()
    {
        Debug.Log("Inside of player menu!!!!!!!!!!!!!!!!!!!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
