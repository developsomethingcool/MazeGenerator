using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    //bool gameEnded = false;
    public string nameOfTheMenu = "";
    public void EndGame()
    {
        Debug.Log("Game over!");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Restart();
    }

    void Restart()
    {
        LoadingSettings.showRespawnMenu = "GameoverMenu";
        nameOfTheMenu = "GameoverMenu";
        SceneManager.LoadScene("Menu");
        MenuController menuController = FindObjectOfType<MenuController>();
        if (menuController != null)
        {
            menuController.ShowGameoverMenu();
        }
    }

    public void Victory()
    {
        nameOfTheMenu = "VictoryMenu";
        LoadingSettings.showRespawnMenu = "VictoryMenu";
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
