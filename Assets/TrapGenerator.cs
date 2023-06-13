using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGenerator : MonoBehaviour
{
    public TrapCell[,] GetTrap()
    {
        TrapCell[,] trap;

        trap = new TrapCell[5, 1];
        for (int x = 0; x < 5; x++)
        {
            trap[x, 0] = new TrapCell(x, 0);
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