using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeRender : MonoBehaviour
{
    [SerializeField] MazeGenerator mazeGenerator;
    [SerializeField] GameObject MazeCellPrefab;
    [SerializeField] TrapGenerator trapGenerator;
    [SerializeField] GoalAreaRender goalArea;


    public NavMeshSurface nms;


    // This the physical size of our maze cells. Getting this wrong will result in overlapping
    // or visible gaps between each cell.
    public float CellSize = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //get our Trap script to place it 
        TrapCell[] trap = trapGenerator.GetTrap();
        // Get our MazeGenerator script to make us a maze.
        MazeCell[,] maze = mazeGenerator.GetMaze();
        //get positions of goal area
        Vector3 newPosition = goalArea.getGoalAreaPosition();

        // Loop through every cell in the maze.
        for (int x = 0; x < mazeGenerator.mazeWidth; x++)
        {
            for (int y = 0; y < mazeGenerator.mazeHeight; y++)
            {
                
                // Instantiate a new maze cell prefab as a child of the MazeRenderer object.
                GameObject newCell = Instantiate(MazeCellPrefab, new Vector3((float)x * CellSize, 0f, (float)y * CellSize), Quaternion.identity);
                // Get a reference to the cell's MazeCellPrefab script.
                MazeCellObject mazeCell = newCell.GetComponent<MazeCellObject>();
                // Determine which walls need to be active.
                bool top = maze[x, y].topWall;
                bool left = maze[x, y].leftWall;
                // Bottom and right walls are deactivated by default unless we are at the bottom or right
                // edge of the maze.
                bool right = false;
                bool bottom = false;
                bool floor = true;
                if (x == mazeGenerator.mazeWidth - 1) right = true;
                if (y == 0) bottom = true;

                
                //floor of Maze is deactivated if its on the same position with trap
                foreach(TrapCell trapCell in trap)
                {
                    if(trapCell.y == y && trapCell.x == x)
                    {
                        floor = false;
                       
                    }
                }

                //make a floor in position of GoalArea invisible
                if (newPosition.y == y && newPosition.x == x)
                {
                    floor = false;

                }



                mazeCell.Init(top, bottom, right, left, floor);
            }
        }

        nms.BuildNavMesh();
    }


}
