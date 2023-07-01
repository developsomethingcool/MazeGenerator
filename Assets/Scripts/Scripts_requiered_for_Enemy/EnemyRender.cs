using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRender : MonoBehaviour
{
    [SerializeField] GameObject EnenemyCellPrefab_Basic;
    [SerializeField] GameObject EnenemyCellPrefab_Axe;
    [SerializeField] MazeGenerator mazeGenerator;

    public float EnemySize = 1f;
    [SerializeField] int enemyAmount = 10;

    void Start()
    {
        for(int i = 0; i < enemyAmount; i++)
        {
            if(Random.Range(0, 2) == 0)
            {
                GameObject newCell = Instantiate(EnenemyCellPrefab_Basic, new Vector3((float)Random.Range(0, mazeGenerator.mazeHeight) * EnemySize, 3f, (float)Random.Range(0, mazeGenerator.mazeHeight) * EnemySize), Quaternion.identity);

            }
            else
            {
                GameObject newCell = Instantiate(EnenemyCellPrefab_Axe, new Vector3((float)Random.Range(0, mazeGenerator.mazeHeight) * EnemySize, 3f, (float)Random.Range(0, mazeGenerator.mazeHeight) * EnemySize), Quaternion.identity);

            }

        }
    }


}
