using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGenerator : MonoBehaviour
{
    //[SerializeField] MazeGenerator mazeGenerator;

    public TrapCell[] GetTrap()
    {
        
        TrapCell[] trap;

        trap = new TrapCell[5];
        for (int x = 0; x < 5; x++)
        {
            trap[x] = new TrapCell(x, 1);
        }
        return trap;

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