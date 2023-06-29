using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAreaRender: MonoBehaviour
{
    [SerializeField] GameObject GoalArea_prefab;
    [SerializeField] MazeGenerator mazeGenerator;

    public float GoalAreaSize = 1f;
    private GameObject newCell;

    void Start()
    {
         newCell = Instantiate(GoalArea_prefab, new Vector3((float)Random.Range(mazeGenerator.mazeHeight-10, mazeGenerator.mazeHeight) * GoalAreaSize, 0f, (float)Random.Range(mazeGenerator.mazeWidth - 10, mazeGenerator.mazeWidth) * GoalAreaSize), Quaternion.identity);
        
    }

   

    public Vector3 getGoalAreaPosition()
    {
        Vector3 newPosition = newCell.transform.position;
        return newPosition;
    }

}
