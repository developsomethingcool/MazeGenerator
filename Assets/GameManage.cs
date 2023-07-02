using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManage : MonoBehaviour
{
    //bool gameEnded = false;
    public string nameOfTheMenu = "";

    //tracker for play time for victory screen

    public void EndGame()
    {
        Debug.Log("Game over!");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Restart();
    }

    void Restart()
    {
        LoadingSettings.showRespawnMenu = "gameoverMenu";
        nameOfTheMenu = "gameoverMenu";
        SceneManager.LoadScene("Menu");
        MenuController menuController = FindObjectOfType<MenuController>();
        if (menuController != null)
        {
            menuController.ShowGameoverMenu();
        }
    }

    public void Victory()
    {
        nameOfTheMenu = "victoryMenu";
        LoadingSettings.showRespawnMenu = "victoryMenu";
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Menu");
        MenuController menuController = FindObjectOfType<MenuController>();
        if (menuController != null)
        {
            menuController.ShowVictoryMenu();
        }
    }
}
