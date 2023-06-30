using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    //bool gameEnded = false;
    public void EndGame()
    {
        Debug.Log("Game over!");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Restart();
    }

    void Restart()
    {
        Debug.Log("Restart activated! Step1");
        LoadingSettings.showRespawnMenu = true;
        Debug.Log("Restart activated! Step2");
        SceneManager.LoadScene("Menu");
        Debug.Log("Restart activated! Step3");
        MenuController menuController = FindObjectOfType<MenuController>();
        Debug.Log("Restart activated! Step4");
        if (menuController != null)
        {
            menuController.ShowGameoverMenu();
        }
        Debug.Log("Restart activated! Step5");
    }
}
