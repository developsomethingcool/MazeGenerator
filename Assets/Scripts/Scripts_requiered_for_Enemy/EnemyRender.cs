using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyRender : MonoBehaviour
{
    //prefabs of the two enemy types implemented
    [SerializeField] GameObject EnenemyCellPrefab_Basic;
    [SerializeField] GameObject EnenemyCellPrefab_Axe;

    //maze generator used to determin the position
    [SerializeField] MazeGenerator mazeGenerator;

    //enemys´Size might be used for more variation in enemys[not implemented]
    public float EnemySize = 1f;

    //enemy amount representing amount of enemys spawned
    [SerializeField] int enemyAmount = 10;

    //Data_percistance carres the options pict in the options menue
    private Data_Percistence dp;

    //enemy multiplyer represent how many enemys should be spawned according to difficulty and mazesize
    private float enemyMultiplyer;

    void Start()
    {
        //getting an instance of data_Perceptience
        dp = new Data_Percistence();

        //setting enemy amount dynamicly base on mazesize and difficuty
        setEnemyAmount();

        //for each enemy we want to spawn
        for(int i = 0; i < enemyAmount; i++)
        {
            //picking a random number between 1 and 0 to determin which enemy type is spawned
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

    //Funktion sets enemy amount based on difficulty and mazesize
    private void setEnemyAmount()
    {
        //calculkating the mazesize
        double mazesize = Math.Pow(mazeGenerator.GetSizeMultiplyer() * dp.getMazeSize(), 2f);

        //swithc  case for each difficulty
        switch (dp.getDifficulty())
        {
            case 1:
                enemyAmount = 1; //on easy the game allways spawns  1 enemy
                break;
            case 2:
                enemyMultiplyer = 0.01f; //The secund difficulty spawns 1 emey for every 100 maze tiles
                enemyAmount = (int) (mazesize * enemyMultiplyer);
                break;
            case 3:
                enemyMultiplyer = 0.02f; //the middel difficulty spawns 2 enemys for every 100 maze tiles
                enemyAmount = (int)(mazesize * enemyMultiplyer);
                break;
            case 4:
                enemyMultiplyer = 0.03f;//the  harder difficulty spwans 3 enemys for every 100 maze tiles
                enemyAmount = (int)(mazesize * enemyMultiplyer);
                break;
            case 5:
                enemyMultiplyer = 0.08f;//the hardes difficulty spawsn 8 enemys for every 100 maze tiles
                enemyAmount = (int)(mazesize * enemyMultiplyer);
                break;
            default:
                enemyMultiplyer = 0.02f;// if ther is an error with numebr the game should defaul to the middle difficulty
                enemyAmount = (int)(mazesize * enemyMultiplyer);
                break;
        }

        //informing the goalarea that we will spawn x enemys
        FindObjectOfType<GoalAreaRender>().UpdateGoalCondition(enemyAmount);
    }

    //getter method for enemy amount
    public int getEnemyAmount()
    {
        return enemyAmount;
    }


}
