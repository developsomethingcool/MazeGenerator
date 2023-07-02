using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

//class used to save data between scences
public class Data_Percistence : MonoBehaviour
{
    //a static instacne
    public static Data_Percistence Instance;

    //the variables which are saved
    public float mazeSize = 3;
    public float difficulty = 3;
    [SerializeField] private string endTime = "0";

    //singulatan used to always have one instance
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //once created it dosn't get destroyed by chaning scenese
        DontDestroyOnLoad(gameObject);
    }

    //setter and getter bas on the static instace
    public void SetMazeSize(float mazeSize)
    {
        Data_Percistence.Instance.mazeSize = mazeSize;
    }

    public void SetDifficulty(float diff)
    {
        Data_Percistence.Instance.difficulty = diff;
    }

    public void SetEndTime(string endTime)
    {
        Data_Percistence.Instance.endTime = endTime;
    }

    public int getMazeSize()
    {
        return (int)Data_Percistence.Instance.mazeSize;
    }

    public int getDifficulty()
    {
        return (int)Data_Percistence.Instance.difficulty;
    }

    public string getEndTime()
    {
        return Data_Percistence.Instance.endTime;
    }

    //start method filling the variables
    private void Start()
    {
        if(Data_Percistence.Instance != null)
        {
            SetMazeSize(Data_Percistence.Instance.mazeSize);
            SetDifficulty(Data_Percistence.Instance.difficulty);
            SetEndTime(Data_Percistence.Instance.endTime);
        }
    }

}
