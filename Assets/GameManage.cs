using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    bool gameEnded = false;
    public void EndGame()
    {
        Debug.Log("Game over!");
        Restart();
    }

    void Restart()
    {
        LoadingSettings.showRespawnMenu = true;

        SceneManager.LoadScene("Menu");

        MenuController menuController = FindObjectOfType<MenuController>();

        if (menuController != null)
            menuController.ShowGameoverMenu();
    }
}
