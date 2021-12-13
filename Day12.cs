namespace AdventOfCode2021;
public sealed class Day12 : Problem
{
    private const string SampleData = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end";
    private const string Data = @"KF-sr
OO-vy
start-FP
FP-end
vy-mi
vy-KF
vy-na
start-sr
FP-lh
sr-FP
na-FP
end-KF
na-mi
lh-KF
end-lh
na-start
wp-KF
mi-KF
vy-sr
vy-lh
sr-mi";
    private const string SampleData2 = @"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc";
    public override void SolvePart1()
    {
        var pathCount = 0;
        var graph = new Graph<string>();
        GraphNode<string> start = null;


        HashSet<string> unvisited = new();
        foreach (var line in Data.Split(Environment.NewLine))
        {
            var split = line.Split("-");
            var n = graph.GetOrAdd(split[0]);
            var c = graph.GetOrAdd(split[1]);
            n.Connections.Add(c);
            c.Connections.Add(n);
            if (split[0] == "start")
            {
                start = n;
            }
        }

        HashSet<string> visited = new();
        Stack<GraphNode<string>> toVisit = new();

        GraphNode<string> current = start;
        pathCount = Traverse(start, visited);

        Console.WriteLine(pathCount);
    }

    private static int Traverse(GraphNode<string> node, HashSet<string> visited)
    {
        if (node.Value == "end")
        {
            return 1;
        }
        if (visited.Contains(node.Value))
        {
            return 0;
        }
        else
        {
            if (char.IsLower(node.Value[0]))
            {
                visited = new HashSet<string>(visited);
                visited.Add(node.Value);
            }
            return node.Connections.Select(n => Traverse(n, visited)).Sum();
        }
    }

    public override void SolvePart2()
    {
        throw new NotImplementedException();
    }
}

public class Graph<T>
{
    private List<GraphNode<T>> _nodes = new();
    public Graph(T rootValue)
    {
        var root = new GraphNode<T>(rootValue);
        _nodes.Add(root);
    }

    public Graph()
    {

    }

    public GraphNode<T> GetOrAdd(T value)
    {
        var existing = _nodes.FirstOrDefault(n => EqualityComparer<T>.Default.Equals(value, n.Value));
        if (existing == null)
        {
            existing = new GraphNode<T>(value);
            _nodes.Add(existing);
        }
        return existing;
    }
}

public class GraphNode<T>
{
    public T? Value { get; private set; }

    public IList<GraphNode<T>> Connections { get; private set; } = new List<GraphNode<T>>();
    public GraphNode(T value)
    {
        Value = value;
    }
}