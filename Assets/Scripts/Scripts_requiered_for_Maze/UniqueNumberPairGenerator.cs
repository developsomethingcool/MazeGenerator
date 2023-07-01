using UnityEngine;
using System.Collections.Generic;

public class UniqueNumberPairGenerator : MonoBehaviour
{
    private static List<(int, int)> generatedPairs = new List<(int, int)>();

    private void Awake()
    {
        generatedPairs.Add((1, 1));
    }

    public static (int, int) GenerateUniqueNumberPair(int minRangeNumber1, int maxRangeNumber1, int minRangeNumber2, int maxRangeNumber2)
    {
        (int number1, int number2) pair;

        do
        {
            pair = GenerateRandomPair(minRangeNumber1, maxRangeNumber1, minRangeNumber2, maxRangeNumber2);
        } while (IsPairGenerated(pair));

        generatedPairs.Add(pair);

        return pair;
    }

    static (int, int) GenerateRandomPair(int minRangeNumber1, int maxRangeNumber1, int minRangeNumber2, int maxRangeNumber2)
    {
        int number1 = Random.Range(minRangeNumber1, maxRangeNumber1);
        int number2 = Random.Range(minRangeNumber2, maxRangeNumber2 );
        return (number1, number2);
    }

    static bool IsPairGenerated((int, int) pair)
    {
        return generatedPairs.Contains(pair);
    }
}
