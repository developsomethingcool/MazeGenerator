using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRender : MonoBehaviour
{
    [SerializeField] TrapGenerator trapGenerator;
    [SerializeField] GameObject TrapCellPrefab;
    public float CellSize = 1f;

    // Start is called before the first frame update
    void Start()
    {
        TrapCell[,] trap = trapGenerator.GetTrap();
        // Loop through every cell in the maze.
        for (int x = 0; x < 5; x++)
        {

            {
                // Instantiate a new maze cell prefab as a child of the MazeRenderer object.
                GameObject newCell = Instantiate(TrapCellPrefab, new Vector3((float)x * CellSize, 0f, (float)0 * CellSize), Quaternion.identity);
                // Get a reference to the cell's MazeCellPrefab script.        
            }

        }
    }
}
