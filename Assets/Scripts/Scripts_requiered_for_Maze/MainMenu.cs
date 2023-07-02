using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject options;
    public GameObject gameoverMenu;

    //Loads GamePlay scene when the game starts
    public void PlayGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Quits the game
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

}
