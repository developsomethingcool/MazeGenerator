using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRender : MonoBehaviour
{
    //mazeGenerator to make floor of maze cells transparent
    //[SerializeField] MazeGenerator mazeGenerator;

    [SerializeField] TrapGenerator trapGenerator;
    //there are a few types of prefabs
    [SerializeField] GameObject TrapCellPrefab_lava;
    [SerializeField] GameObject TrapCellPrefab_cylinder;
    [SerializeField] GameObject TrapCellPrefab_sphere;

    public float CellSize = 1f;
    public int trapCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        TrapCell[] trap = trapGenerator.GetterTrap();
        int[] trapType = trapGenerator.GetterTrapType();
        // Loop through every cell in the maze.

        //get maze to modify visibility of floor
        //MazeCell[,] maze = mazeGenerator.GetMaze();

        for (int x = 0; x < trap.Length; x++)
        {   
                // Instantiate a new maze cell prefab as a child of the MazeRenderer object.
            if(trapType[x] == 0){
                GameObject newTrap = Instantiate(TrapCellPrefab_lava, new Vector3((float)trap[x].x * CellSize, -1f, (float)trap[x].y * CellSize), Quaternion.identity);

            }if (trapType[x] == 1)
            {
                GameObject newTrap = Instantiate(TrapCellPrefab_cylinder, new Vector3((float)trap[x].x * CellSize, 0f, (float)trap[x].y * CellSize), Quaternion.identity);

            }
            if (trapType[x] == 2)
            {
                GameObject newTrap = Instantiate(TrapCellPrefab_sphere, new Vector3((float)trap[x].x * CellSize, 0f, (float)trap[x].y * CellSize), Quaternion.identity);

            }
       
        }

    }
}
