using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAreaRender: MonoBehaviour
{
    [SerializeField] GameObject GoalArea_prefab;
    [SerializeField] MazeGenerator mazeGenerator;

    public float GoalAreaSize = 1f;
    private GameObject newCell;

    //positions of GoalArea
    private float x;
    private float y;
    private float z;

    //number of already killed enemies
    private int enemiesKilled = 0;

    //number of enemies to win a game
    public int numberOfKilledEnemiesToWin = 5;

    void Start()
    {
        x = (float)Random.Range(mazeGenerator.GetMazeHeight() - 10, mazeGenerator.GetMazeHeight());
        y = 0f;
        z = (float)Random.Range(mazeGenerator.GetMazeHeight() - 10, mazeGenerator.GetMazeHeight());
        newCell = Instantiate(GoalArea_prefab, new Vector3(x * GoalAreaSize, y, y * GoalAreaSize), Quaternion.identity);
        
    }

    private void Update()
    {
        if(enemiesKilled == numberOfKilledEnemiesToWin)
        {
            updateGoalArea();
            LoadingSettings.GoalAreaOpened = true;

        }

    }

    public Vector3 getGoalAreaPosition()
    {
        Vector3 newPosition = newCell.transform.position;
        return newPosition;
    }

    public void updateGoalArea()
    {
        Material goalAreaMaterial = new Material(Shader.Find("Standard"));

        // Set the color of the material
        goalAreaMaterial.color = new Color32(0, 255, 0, 255); // make a finish green
       
        // Access the renderer component of the prefab
        Renderer prefabRenderer = newCell.GetComponent<Renderer>();

        // Update the color of the goal area
        prefabRenderer.material = goalAreaMaterial;

        GameObject.Find("FinishingArea").GetComponent<Renderer>().material = goalAreaMaterial;
    }

    public void enemyKilled()
    {
        Debug.Log("numberKilledEnemies:" + enemiesKilled);
        enemiesKilled++;
    }


}
