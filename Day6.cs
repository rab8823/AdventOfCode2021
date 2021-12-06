using System.Linq;

namespace AdventOfCode2021;

public sealed class Day6 : Problem
{
    private const string SampleData = "3,4,3,1,2";
    private const string Data = "1,2,1,1,1,1,1,1,2,1,3,1,1,1,1,3,1,1,1,5,1,1,1,4,5,1,1,1,3,4,1,1,1,1,1,1,1,5,1,4,1,1,1,1,1,1,1,5,1,3,1,3,1,1,1,5,1,1,1,1,1,5,4,1,2,4,4,1,1,1,1,1,5,1,1,1,1,1,5,4,3,1,1,1,1,1,1,1,5,1,3,1,4,1,1,3,1,1,1,1,1,1,2,1,4,1,3,1,1,1,1,1,5,1,1,1,2,1,1,1,1,2,1,1,1,1,4,1,3,1,1,1,1,1,1,1,1,5,1,1,4,1,1,1,1,1,3,1,3,3,1,1,1,2,1,1,1,1,1,1,1,1,1,5,1,1,1,1,5,1,1,1,1,2,1,1,1,4,1,1,1,2,3,1,1,1,1,1,1,1,1,2,1,1,1,2,3,1,2,1,1,5,4,1,1,2,1,1,1,3,1,4,1,1,1,1,3,1,2,5,1,1,1,5,1,1,1,1,1,4,1,1,4,1,1,1,2,2,2,2,4,3,1,1,3,1,1,1,1,1,1,2,2,1,1,4,2,1,4,1,1,1,1,1,5,1,1,4,2,1,1,2,5,4,2,1,1,1,1,4,2,3,5,2,1,5,1,3,1,1,5,1,1,4,5,1,1,1,1,4";
    public override void SolvePart1()
    {
        Console.WriteLine(SolveSmart(SampleData, 80));
    }

    private static List<int> ReadData(string rawData)
    {
        return rawData.Split(',').Select(int.Parse).ToList();
    }

    private static int SolvePart1BruteForce(string input)
    {
        var data = ReadData(input);
        for (var i = 0; i < 256; i++)
        {
            var startingEndCount = data.Count;
            for (var d = 0; d < startingEndCount; d++)
            {
                if (data[d] == 0)
                {
                    data[d] = 6;
                    data.Add(8);
                }
                else
                {
                    data[d]--;
                }
            }
        }
        return data.Count;
    }

    private static long SolveSmart(string input, int days)
    {
        var data = ReadData(input);

        var transformedData2 = Enumerable.Repeat(0L, 9).ToList();
        foreach (var item in data)
        {
            transformedData2[item]++;
        }

        for (var d = 0; d < days; d++)
        {
            var toAdd = transformedData2[0];
            for (var i = 1; i < transformedData2.Count; i++)
            {
                transformedData2[i - 1] = transformedData2[i];
            }
            transformedData2[8] = toAdd;
            transformedData2[6] = transformedData2[6] + toAdd;
        }

        return transformedData2.Sum();
    }

    public override void SolvePart2()
    {
        Console.WriteLine(SolveSmart(Data, 256));
    }
}