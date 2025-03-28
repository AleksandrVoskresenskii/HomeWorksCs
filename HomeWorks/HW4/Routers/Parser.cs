namespace Routers;

public class Parser
{
    /// <summary>
    /// Разбирает одну строку из файла и возвращает список рёбер.
    /// Формат строки: 1: 2 (10), 3 (5)
    /// </summary>
    public static List<Edge> ParseLine(string line)
    {
        var edges = new List<Edge>();
        var parts = line.Split(':');
        int from = int.Parse(parts[0].Trim());

        if (parts.Length < 2 || string.IsNullOrWhiteSpace(parts[1]))
        {
            return edges;
        }

        var connections = parts[1].Split(',');
        foreach (var conn in connections)
        {
            var tokens = conn.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length < 2)
            {
                continue; // Пропускаем некорректный формат
            }

            int to = int.Parse(tokens[0]);
            int capacity = int.Parse(tokens[1].Trim('(', ')'));
            edges.Add(new Edge(from, to, capacity));
        }

        return edges;
    }

    /// <summary>
    /// Заглушка для парсинга из файла. Реализуется при необходимости.
    /// </summary>
    internal static List<Edge> ParseFromFile(string v)
    {
        throw new NotImplementedException();
    }
}