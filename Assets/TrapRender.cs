using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRender : MonoBehaviour
{
    [SerializeField] TrapGenerator trapGenerator;
    [SerializeField] GameObject TrapCellPrefab;
    public float CellSize = 1f;
    public int trapCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        TrapCell[] trap = trapGenerator.GetTrap();
        // Loop through every cell in the maze.

        for (int x = 0; x < trap.Length; x++)
        {   
                // Instantiate a new maze cell prefab as a child of the MazeRenderer object.
                GameObject newTrap = Instantiate(TrapCellPrefab, new Vector3((float)trap[x].x * CellSize, 0f, (float)trap[x].y * CellSize), Quaternion.identity);
                // Get a reference to the cell's MazeCellPrefab script.
                //TrapCellObject trapCell = newTrap.GetComponent<TrapCellObject>();
                //trapCell.Init(true, true, true, true);
                newTrap.name = "Trap" + trapCounter.ToString();
                trapCounter++;
                //Debug.Log(x);
        }
    }
}
