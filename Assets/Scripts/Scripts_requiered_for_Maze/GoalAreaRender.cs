using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAreaRender: MonoBehaviour
{
    [SerializeField] GameObject GoalArea_prefab;
    [SerializeField] MazeGenerator mazeGenerator;
    [SerializeField] TextModifier goalText;
    [SerializeField] int killCount = 0;
    [SerializeField] int enemysTotal = 0;

    public float GoalAreaSize = 1f;
    private GameObject newCell;
    private bool goalUnlocked = false;

    //positions of GoalArea
    private int x = 0;
    private int y = 0;
    private int z = 0;

    void Start()
    {
        (x, z) = UniqueNumberPairGenerator.GenerateUniqueNumberPair(mazeGenerator.GetMazeHeight()/2, mazeGenerator.GetMazeHeight(), mazeGenerator.GetMazeWidth()/2, mazeGenerator.GetMazeWidth());

        newCell = Instantiate(GoalArea_prefab, new Vector3(x * GoalAreaSize, y, z * GoalAreaSize), Quaternion.identity);

    }

    private void Update()
    {
        if (!goalUnlocked)
        {
            if (killCount >= enemysTotal)
            {
                updateGoalArea();
            }
        }
        
    }



    public Vector3 getGoalAreaPosition()
    {
        Vector3 newPosition = newCell.transform.position;
        return newPosition;
    }

    public void updateGoalArea()
    {
        goalText.UpdateText("All Enemys Killed\nReach the goal!");

        goalUnlocked = true;

        Material goalAreaMaterial = new Material(Shader.Find("Standard"));

        // Set the color of the material
        goalAreaMaterial.color = new Color32(0, 255, 0, 255); // make a finish green
       
        Debug.Log("Updated color of GoalArea");
        // Access the renderer component of the prefab
        Renderer prefabRenderer = newCell.GetComponent<Renderer>();

        // Update the color of the goal area
        prefabRenderer.material = goalAreaMaterial;

        GameObject.Find("FinishingArea").GetComponent<Renderer>().material = goalAreaMaterial;
            


    }

    public void EnemyKilled()
    {
        killCount++;
        goalText.UpdateText(killCount, enemysTotal);
    }

    public void UpdateGoalCondition(int eT)
    {
        enemysTotal = eT;
        goalText.UpdateText(0, eT);
    }

    public bool goalReached()
    {
        return goalUnlocked;
    }

}
