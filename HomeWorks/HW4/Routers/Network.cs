namespace Routers;

public class Network(List<Edge> edges)
{
    private List<Edge> edges = edges;
    private HashSet<int> nodes = new HashSet<int>(edges.SelectMany(e => new[] { e.From, e.To }));

    public bool IsConnected()
    {
        var visited = new HashSet<int>();
        void DFS(int node)
        {
            if (!visited.Add(node)) return;
            foreach (var e in edges)
            {
                if (e.From == node)
                {
                    DFS(e.To);
                }
                else if (e.To == node)
                {
                    DFS(e.From);
                }
            }
        }

        if (!nodes.Any()) return false;
        DFS(nodes.First());
        return visited.Count == nodes.Count;
    }

    public List<Edge> BuildMaximumSpanningTree()
    {
        var parent = nodes.ToDictionary(n => n, n => n);

        int Find(int x)
        {
            if (parent[x] != x)
            {
                parent[x] = Find(parent[x]);
            }

            return parent[x];
        }

        void Union(int a, int b) => parent[Find(a)] = Find(b);

        var mst = new List<Edge>();
        foreach (var edge in edges.OrderByDescending(e => e.Capacity))
        {
            if (Find(edge.From) != Find(edge.To))
            {
                mst.Add(edge);
                Union(edge.From, edge.To);
            }
        }
        return mst;
    }

    public static void WriteConfiguration(List<Edge> mst, string outputPath)
    {
        var config = new Dictionary<int, List<Edge>>();

        foreach (var edge in mst)
        {
            if (!config.ContainsKey(edge.From)) config[edge.From] = new();
            if (!config.ContainsKey(edge.To)) config[edge.To] = new();
            config[edge.From].Add(edge);
            config[edge.To].Add(new Edge(edge.To, edge.From, edge.Capacity));
        }

        using var writer = new StreamWriter(outputPath);
        foreach (var node in config.Keys.OrderBy(n => n))
        {
            var connections = string.Join(", ", config[node]
                .OrderBy(e => e.To)
                .Select(e => $"{e.To} ({e.Capacity})"));
            writer.WriteLine($"{node}: {connections}");
        }
    }
}