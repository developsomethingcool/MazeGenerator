using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Data_Percistence : MonoBehaviour
{
    public static Data_Percistence Instance;

    public float mazeSize = 3;
    public float difficulty = 3;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetMazeSize(float mazeSize)
    {
        Data_Percistence.Instance.mazeSize = mazeSize;
    }

    public void SetDifficulty(float diff)
    {
        Data_Percistence.Instance.difficulty = diff;
    }

    public int getMazeSize()
    {
        return (int)Data_Percistence.Instance.mazeSize;
    }

    public int getDifficulty()
    {
        return (int)Data_Percistence.Instance.difficulty;
    }

    private void Start()
    {
        if(Data_Percistence.Instance != null)
        {
            SetMazeSize(Data_Percistence.Instance.mazeSize);
            SetDifficulty(Data_Percistence.Instance.difficulty);
        }
    }

}