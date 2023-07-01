
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalAreaRender: MonoBehaviour
{
    //prefab of goal area
    [SerializeField] GameObject GoalArea_prefab;
    [SerializeField] MazeGenerator mazeGenerator;
    [SerializeField] int killCount = 0;
    [SerializeField] int enemysTotal = 0;

    public float GoalAreaSize = 1f;
    private GameObject newCell;
    private GameObject dPointer;
    private bool goalUnlocked = false;

    //positions of GoalArea
    private float x;
    private float y;
    private float z;

    //
    private TextMeshProUGUI textMeshPro;
    int counter = 0;

    void Start()
    {
        x = (float)Random.Range(mazeGenerator.GetMazeHeight() - 10, mazeGenerator.GetMazeHeight());
        y = 0f;
        z = (float)Random.Range(mazeGenerator.GetMazeHeight() - 10, mazeGenerator.GetMazeHeight());
        newCell = Instantiate(GoalArea_prefab, new Vector3(x * GoalAreaSize, y, y * GoalAreaSize), Quaternion.identity);
        textMeshPro = newCell.GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.enabled = false;

    }

    private void Update()
    {
        if (!goalUnlocked)
        {
            if (killCount >= enemysTotal)
            {
                updateGoalArea();
                textMeshPro.enabled = true;

            }
        }
        textMeshPro.text = distanceBetweenPlayerAndPoint().ToString();
        
        
    }



    public Vector3 getGoalAreaPosition()
    {
        Vector3 newPosition = newCell.transform.position;
        return newPosition;
    }

    public void updateGoalArea()
    {
        FindObjectOfType<TextModifier>().UpdateText("All Enemys Killed\nReach the goal!");

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
        FindObjectOfType<TextModifier>().UpdateText(killCount, enemysTotal);
    }

    public void UpdateGoalCondition(int eT)
    {
        enemysTotal = eT;
        FindObjectOfType<TextModifier>().UpdateText(0, eT);
    }

    public bool goalReached()
    {
        return goalUnlocked;
    }

    public float distanceBetweenPlayerAndPoint()
    {
        float distanceSquared = Mathf.Pow((FindObjectOfType<PlayerMovment>().playersPosition().x - x),2) + Mathf.Pow((FindObjectOfType<PlayerMovment>().playersPosition().z -z),2);
        float distance = Mathf.Sqrt(distanceSquared);
        return distance;
    }

}
