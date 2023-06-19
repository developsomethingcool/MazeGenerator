using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGenerator : MonoBehaviour
{
    [SerializeField] MazeGenerator mazeGenerator;
    public int mazeWidth;
    public int mazeLength;
    int numberOfTraps = 10;
    private TrapCell[] cells;
    public TrapGenerator instance;

    /*private void Start()
    {
        cells = GetTrap();
    } */

    public TrapGenerator()
    {

    }

   /* void Awake()
    {
        Debug.Log("Wake up method functions!");
    }*/

    public TrapCell[] GetTrap()
    {
        //get parameters from Maze
        MazeCell[,] maze = mazeGenerator.GetMaze();

        mazeLength = maze.GetLength(0);
        mazeWidth = maze.GetLength(1);

        //generate indices for traps
        TrapCell[] positionsOfTraps = new TrapCell[numberOfTraps];
        TrapCell p;
        for (int i = 0; i < numberOfTraps; i++)
        {
            p = new TrapCell(Random.Range(0, mazeLength), Random.Range(0, mazeWidth));
            //p = new TrapCell(1+i,1);
            positionsOfTraps[i] = p;
        }


        /*
        trap = new TrapCell[numberOfTraps];
        for (int x = 0; x < numberOfTraps; x++)
        {
            trap[x] = new TrapCell(positionsOfTraps[x].x, positionsOfTraps[x].y);
        }*/
        this.cells = positionsOfTraps;
        return  positionsOfTraps;

    }

    public TrapCell[] GetterTrap(){
        return this.cells;
    }

}

public class TrapCell
{
    public int x, y;
    // Return x and y as a Vector2Int for convenience sake.
    public Vector2Int position
    {
        get
        {
            return new Vector2Int(x, y);
        }
    }

    public TrapCell(int x, int y)
    {
        // The coordinates of this cell in the maze grid.
        this.x = x;
        this.y = y;

    }

}

//class of coordinated of each Poit
public class Point
{
    public int x;
    public int y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int getX() {
        return this.x;
    }
    public int getY()
    {
        return this.y;
    }

}