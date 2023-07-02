using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAreaRender: MonoBehaviour
{
    //Goal area prefab
    [SerializeField] GameObject GoalArea_prefab;

    //mazegenerator to calcuate postion of goalarea
    [SerializeField] MazeGenerator mazeGenerator;

    //text modifyer for the goal Text
    [SerializeField] TextModifier goalText;

    //kill count of current enemys killed
    [SerializeField] int killCount = 0;
    //variable saving the total amount of enemys
    [SerializeField] int enemysTotal = 0;

    //goal area
    public float GoalAreaSize = 1f;
    private GameObject newCell;

    //The goal only unlocks after all enmys were killed
    private bool goalUnlocked = false;

    //positions of GoalArea
    private int x = 0;
    private int y = 0;
    private int z = 0;

    void Start()
    {
        //creating a unique number pair. this is needed to not spawn two traps or the goal are and a trap onto of eachother
        (x, z) = UniqueNumberPairGenerator.GenerateUniqueNumberPair(mazeGenerator.GetMazeHeight()/2, mazeGenerator.GetMazeHeight(), mazeGenerator.GetMazeWidth()/2, mazeGenerator.GetMazeWidth());

        //instanciating the goal area
        newCell = Instantiate(GoalArea_prefab, new Vector3(x * GoalAreaSize, y, z * GoalAreaSize), Quaternion.identity);

    }

    //update loop chekcing if the goal area should be unlocked
    private void Update()
    {
        //if it is not yet unlocked
        if (!goalUnlocked)
        {
            //we check if the player killed enoght enemys
            if (killCount >= enemysTotal)
            {
                //if yes we unlock the goalarea
                updateGoalArea();
            }
        }
        
    }


    //getter method for the goalArea position
    public Vector3 getGoalAreaPosition()
    {
        Vector3 newPosition = newCell.transform.position;
        return newPosition;
    }

    //updating/unlocking the goal area
    public void updateGoalArea()
    {
        //updating the goal text on the player ui
        goalText.UpdateText("All Enemys Killed\nReach the goal!");

        //save that it is unlockt
        goalUnlocked = true;

        //finding the matirial of the goal area
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
