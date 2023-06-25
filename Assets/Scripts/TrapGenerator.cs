using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGenerator : MonoBehaviour
{
    [SerializeField] MazeGenerator mazeGenerator;
    public int mazeWidth;
    public int mazeLength;
    int numberOfTraps = 40;
    //store traps
    private TrapCell[] cells;
    //store types of the traps
    private int[] typesTraps;


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
        int[] typesTraps = new int[numberOfTraps];
        TrapCell p;
        
        for (int i = 0; i < numberOfTraps; i++)
        {
            p = new TrapCell(Random.Range(0, mazeLength), Random.Range(0, mazeWidth));
            positionsOfTraps[i] = p;
            Debug.Log("Number outputed!");
            typesTraps[i] = Random.Range(0, 3);
            

        }

        this.cells = positionsOfTraps;
        this.typesTraps = typesTraps;
        return  positionsOfTraps;

    }

    public TrapCell[] GetterTrap(){
        return this.cells;
    }
    public int[] GetterTrapType()
    {
        return this.typesTraps;
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
