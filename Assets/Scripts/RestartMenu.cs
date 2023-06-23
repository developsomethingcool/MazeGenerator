using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    public void PlayGame()
    {
       Debug.Log("Inside of player menu!!!!!!!!!!!!!!!!!!!");

       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       
    }
}
