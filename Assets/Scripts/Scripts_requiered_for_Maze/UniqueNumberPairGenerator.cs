using UnityEngine;
using System.Collections.Generic;

public class UniqueNumberPairGenerator : MonoBehaviour
{
    //A list of all pairs aöready created by this method
    private static List<(int, int)> generatedPairs = new List<(int, int)>();

    //Awake
    private void Awake()
    {
        //Add pair 1/1 so nothings spawns on the plays spawn position
        generatedPairs.Add((1, 1));
    }

    //Mehotd generating a new random pair
    public static (int, int) GenerateUniqueNumberPair(int minRangeNumber1, int maxRangeNumber1, int minRangeNumber2, int maxRangeNumber2)
    {
        //creating the pair
        (int number1, int number2) pair;

        //generate a new pair till you generate a pai not yet generated
        do
        {
            //generaing a new pair
            pair = GenerateRandomPair(minRangeNumber1, maxRangeNumber1, minRangeNumber2, maxRangeNumber2);
        //Checking that this pair was not yet created
        } while (IsPairGenerated(pair));

        //add the unique pair to the list
        generatedPairs.Add(pair);

        //return the pair
        return pair;
    }

    //Aktual method creating two random ints in a given range
    static (int, int) GenerateRandomPair(int minRangeNumber1, int maxRangeNumber1, int minRangeNumber2, int maxRangeNumber2)
    {
        //creating both numbers
        int number1 = Random.Range(minRangeNumber1, maxRangeNumber1);
        int number2 = Random.Range(minRangeNumber2, maxRangeNumber2 );
        //returning them
        return (number1, number2);
    }

    //method checks that the pair we check agains is not yet in the list
    static bool IsPairGenerated((int, int) pair)
    {
        return generatedPairs.Contains(pair);
    }
}
