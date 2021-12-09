namespace AdventOfCode2021;
public static class Utils
{
    public static IEnumerable<string> SplitLines(string data)
    {
        return data.Split(Environment.NewLine);
    }
}