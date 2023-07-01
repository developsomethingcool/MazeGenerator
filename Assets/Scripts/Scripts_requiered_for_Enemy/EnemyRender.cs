using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyRender : MonoBehaviour
{
    [SerializeField] GameObject EnenemyCellPrefab_Basic;
    [SerializeField] GameObject EnenemyCellPrefab_Axe;
    [SerializeField] MazeGenerator mazeGenerator;

    public float EnemySize = 1f;
    [SerializeField] int enemyAmount = 10;

    private Data_Percistence dp;
    private float enemyMultiplyer;

    void Start()
    {

        dp = new Data_Percistence();
        setEnemyAmount();


        for(int i = 0; i < enemyAmount; i++)
        {
            if(Random.Range(0, 2) == 0)
            {
                GameObject newCell = Instantiate(EnenemyCellPrefab_Basic, new Vector3((float)Random.Range(0, mazeGenerator.GetMazeHeight()) * EnemySize, 3f, (float)Random.Range(0, mazeGenerator.GetMazeHeight()) * EnemySize), Quaternion.identity);

            }
            else
            {
                GameObject newCell = Instantiate(EnenemyCellPrefab_Axe, new Vector3((float)Random.Range(0, mazeGenerator.GetMazeHeight()) * EnemySize, 3f, (float)Random.Range(0, mazeGenerator.GetMazeHeight()) * EnemySize), Quaternion.identity);  

            }

        }
    }

    private void setEnemyAmount()
    {
        double mazesize = Math.Pow(mazeGenerator.GetSizeMultiplyer() * dp.getMazeSize(), 2f);

        switch (dp.getDifficulty())
        {
            case 0:
                enemyAmount = 1;
                break;
            case 1:
                enemyMultiplyer = 0.01f;
                enemyAmount = (int) (mazesize * enemyMultiplyer);
                break;
            case 2:
                enemyMultiplyer = 0.02f;
                enemyAmount = (int)(mazesize * enemyMultiplyer);
                break;
            case 3:
                enemyMultiplyer = 0.03f;
                enemyAmount = (int)(mazesize * enemyMultiplyer);
                break;
            case 4:
                enemyMultiplyer = 0.08f;
                enemyAmount = (int)(mazesize * enemyMultiplyer);
                break;
            default:
                enemyMultiplyer = 0.02f;
                enemyAmount = (int)(mazesize * enemyMultiplyer);
                break;
        }
    }


}
