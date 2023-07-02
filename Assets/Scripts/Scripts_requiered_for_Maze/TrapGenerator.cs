using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrapGenerator : MonoBehaviour
{
    //mazegenerator needed to determin troa position
    [SerializeField] MazeGenerator mazeGenerator;
    //maze width and lenght to place the traps
    public int mazeWidth;
    public int mazeLength;

    //number of traps
    [SerializeField] int numberOfTraps = 80;
    //store traps
    private TrapCell[] cells;
    //store types of the traps
    private int[] typesTraps;

    //data percistance(stores options pickt) and drapmuliplyer both used to dertmin amount of traps dynamicly
    private Data_Percistence dp;
    private float trapMultiplyer;



    public TrapGenerator()
    {

    }

    private void Awake()
     {
        try
        {
            //getting the data_percistance instance
            dp = new Data_Percistence();
            //setting the trapMultiplyer
            setTrapMultiplyer();
            //claculating the maze size
            double mazesize = Math.Pow(mazeGenerator.GetSizeMultiplyer() * dp.getMazeSize(), 2f);
            //calculating the amount of traps
            numberOfTraps = (int)Math.Round(mazesize * trapMultiplyer);
              
        }
        catch (System.Exception e)
        {
            Debug.Log("No Data_Percistence gefunden, wahrscheinlich mit gameplay Scene angefangen, wenn ja dann ist das normal");
        }
    }

    //Mehtod used to set the trapmultiplyer
    private void setTrapMultiplyer()
    {
        //Swithc case foir all options
        switch (dp.getDifficulty())
        {
            case 1:
                trapMultiplyer = 0.07f; //easy = 7% traps
                break;
            case 2:
                trapMultiplyer = 0.08f;//mediusm-easy = 8% traps
                break;
            case 3:
                trapMultiplyer = 0.09f;//medium = 9% traps
                break;
            case 4:
                trapMultiplyer = 0.1f;//medium-hard = 10% traps
                break;
            case 5:
                trapMultiplyer = 0.2f;//hard = 20% traps
                break;
            default:
                trapMultiplyer = 0.09f;//default is mediusm(only occures by errors in code)
                break;
        }
    }

    public TrapCell[] GetTrap()
    {
        //get parameters from Maze
        MazeCell[,] maze = mazeGenerator.GetMaze();

        //geting maze lenght and width
        mazeLength = maze.GetLength(0);
        mazeWidth = maze.GetLength(1);

        //generate indices for traps
        TrapCell[] positionsOfTraps = new TrapCell[numberOfTraps];
        int[] typesTraps = new int[numberOfTraps];
        TrapCell p;

        for (int i = 0; i < numberOfTraps; i++)
        {
            (int number1, int number2) = UniqueNumberPairGenerator.GenerateUniqueNumberPair(0, mazeLength, 0, mazeWidth);
            p = new TrapCell(number1, number2);
            positionsOfTraps[i] = p;

            typesTraps[i] = Random.Range(0, 3);


        }

        this.cells = positionsOfTraps;
        this.typesTraps = typesTraps;
        return positionsOfTraps;

    }

    public TrapCell[] GetterTrap()
    {
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

