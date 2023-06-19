using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRender : MonoBehaviour
{
    //mazeGenerator to make floor of maze cells transparent
    //[SerializeField] MazeGenerator mazeGenerator;

    [SerializeField] TrapGenerator trapGenerator;
    [SerializeField] GameObject TrapCellPrefab;
    public float CellSize = 1f;
    public int trapCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        TrapCell[] trap = trapGenerator.GetterTrap();
        // Loop through every cell in the maze.

        //get maze to modify visibility of floor
        //MazeCell[,] maze = mazeGenerator.GetMaze();

        for (int x = 0; x < trap.Length; x++)
        {   
                // Instantiate a new maze cell prefab as a child of the MazeRenderer object.
                GameObject newTrap = Instantiate(TrapCellPrefab, new Vector3((float)trap[x].x * CellSize, -1f, (float)trap[x].y * CellSize), Quaternion.identity);
            Debug.Log("This particular MazeCell has to be modified!");  
            //maze[trap[x].x, trap[x].y].setVisibility(false);
                // Get a reference to the cell's MazeCellPrefab script.
                //TrapCellObject trapCell = newTrap.GetComponent<TrapCellObject>();
                //trapCell.Init(true, true, true, true);
                newTrap.name = "Trap" + trapCounter.ToString();
                trapCounter++;        
        }

    }
}
