using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    bool gameEnded = false;
    public void EndGame()
    {
        if (gameEnded == false)
        {
            gameEnded = true;
            Debug.Log("Game over!");
            Restart();
        }

    }

    void Restart()
    {
        LoadingSettings.showRespawnMenu = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        MenuController menuController = FindObjectOfType<MenuController>();

        if (menuController != null)
            menuController.ShowGameoverMenu();
    }
}
