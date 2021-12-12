namespace AdventOfCode2021;
public sealed class Day11 : Problem
{
    private const string SampleData = @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526";
    private const string Data = @"2344671212
6611742681
5575575573
3167848536
1353827311
4416463266
2624761615
1786561263
3622643215
4143284653";
    public override void SolvePart1()
    {
        // count flashes after 100 steps
        var data = ParseData(Data);
        var flashes = 0;
        for (var i = 0; i < 100; i++)
        {
            flashes += Step(data);
        }
        Console.WriteLine(flashes);
    }

    private static int[,] ParseData(string data)
    {
        var parsed = new int[10, 10];
        var lines = data.Split(Environment.NewLine);
        for (var r = 0; r < 10; r++)
        {
            var row = lines[r];
            for (var c = 0; c < 10; c++)
            {
                parsed[r, c] = row[c] - 48;
            }
        }
        return parsed;
    }

    private static int Step(int[,] data)
    {
        for (var r = 0; r < data.GetLength(0); r++)
        {
            for (var c = 0; c < data.GetLength(1); c++)
            {
                data[r, c] = data[r, c] + 1;
            }
        }

        var updated = true;
        do
        {
            updated = TryUpdate(data);
            PrintData(data);
        } while (updated);

        var flashes = CountFlashesAndReset(data);
        PrintData(data);
        return flashes;
    }

    private static void PrintData(int[,] data)
    {
        for (var r = 0; r < data.GetLength(0); r++)
        {
            for (var c = 0; c < data.GetLength(1); c++)
            {
                Console.Write(data[r, c] > 9 ? 0 : data[r, c]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    private static int CountFlashesAndReset(int[,] data)
    {
        var flashes = 0;
        for (var r = 0; r < data.GetLength(0); r++)
        {
            for (var c = 0; c < data.GetLength(1); c++)
            {
                if (data[r, c] == -1)
                {
                    data[r, c] = 0;
                    flashes++;
                }
            }
        }
        return flashes;
    }

    private static bool TryUpdate(int[,] data)
    {
        var updated = false;
        for (var r = 0; r < data.GetLength(0); r++)
        {
            for (var c = 0; c < data.GetLength(1); c++)
            {
                if (data[r, c] > 9)
                {
                    data[r, c] = -1;
                    UpdateAdjacent(data, r, c);
                    updated = true;
                }
            }
        }
        return updated;
    }

    private static void UpdateAdjacent(int[,] data, int r, int c)
    {
        int row = r, col = c;
        if (InBounds(data, --row, col) && data[row, col] > -1)
        { // up
            data[row, col] = data[row, col] + 1;
        }
        row = r;
        col = c;
        if (InBounds(data, --row, ++col) && data[row, col] > -1)
        { // up right
            data[row, col] = data[row, col] + 1;
        }
        row = r;
        col = c;
        if (InBounds(data, row, ++col) && data[row, col] > -1)
        { // right
            data[row, col] = data[row, col] + 1;
        }
        row = r;
        col = c;
        if (InBounds(data, ++row, ++col) && data[row, col] > -1)
        { // down right
            data[row, col] = data[row, col] + 1;
        }
        row = r;
        col = c;
        if (InBounds(data, ++row, col) && data[row, col] > -1)
        { // down 
            data[row, col] = data[row, col] + 1;
        }
        row = r;
        col = c;
        if (InBounds(data, ++row, --col) && data[row, col] > -1)
        { // down left
            data[row, col] = data[row, col] + 1;
        }
        row = r;
        col = c;
        if (InBounds(data, row, --col) && data[row, col] > -1)
        { // left
            data[row, col] = data[row, col] + 1;
        }
        row = r;
        col = c;
        if (InBounds(data, --row, --col) && data[row, col] > -1)
        { // up left
            data[row, col] = data[row, col] + 1;
        }
    }

    private static bool InBounds(int[,] data, int r, int c)
    {
        return r >= 0 && r < data.GetLength(0) && c >= 0 && c < data.GetLength(1);
    }

    public override void SolvePart2()
    {
        // count steps until all flash
        var data = ParseData(Data);
        var steps = 1;
        while (Step(data) < 100 && steps < 2000)
        {
            steps++;
        }
        Console.WriteLine(steps);
    }
}