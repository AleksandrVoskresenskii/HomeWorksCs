namespace Routers;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: dotnet run <input_file> <output_file>");
            Environment.Exit(1);
        }

        string inputPath = args[0];
        string outputPath = args[1];

        try
        {
            var allEdges = new List<Edge>();
            var lines = File.ReadAllLines(inputPath);

            foreach (var line in lines)
            {
                var parsed = Parser.ParseLine(line);
                allEdges.AddRange(parsed);
            }

            var network = new Network(allEdges);

            if (!network.IsConnected())
            {
                Console.Error.WriteLine("Сеть не связна");
                Environment.Exit(1);
            }

            var mst = network.BuildMaximumSpanningTree();

            var output = new Dictionary<int, List<Edge>>();

            foreach (var edge in mst)
            {
                if (!output.ContainsKey(edge.From))
                    output[edge.From] = new List<Edge>();
                output[edge.From].Add(edge);
            }

            using var writer = new StreamWriter(outputPath);
            foreach (var from in output.Keys)
            {
                var edges = output[from];
                var parts = new List<string>();

                foreach (var edge in edges)
                {
                    parts.Add($"{edge.To} ({edge.Capacity})");
                }

                writer.WriteLine($"{from}: {string.Join(", ", parts)}");
            }

            Console.WriteLine("Конфигурация успешно сохранена в файл.");
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Ошибка: {e.Message}");
            Environment.Exit(1);
        }
    }
}