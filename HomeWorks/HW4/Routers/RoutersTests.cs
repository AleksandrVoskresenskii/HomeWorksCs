namespace Routers.Tests;

using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class NetworkTests
{
    [Test]
    public void Edge_CreatesCorrectEdge()
    {
        var edge = new Edge(1, 2, 10);
        Assert.That(edge.From, Is.EqualTo(1));
        Assert.That(edge.To, Is.EqualTo(2));
        Assert.That(edge.Capacity, Is.EqualTo(10));
    }

    [Test]
    public void IsConnected_ReturnsTrue_WhenGraphConnected()
    {
        var edges = new List<Edge>
        {
            new Edge(1, 2, 5),
            new Edge(2, 3, 5),
        };

        var network = new Network(edges);
        Assert.That(network.IsConnected(), Is.True);
    }

    [Test]
    public void IsConnected_ReturnsFalse_WhenGraphDisconnected()
    {
        var edges = new List<Edge>
        {
            new Edge(1, 2, 5),
            new Edge(3, 4, 5),
        };

        var network = new Network(edges);
        Assert.That(network.IsConnected(), Is.False);
    }

    [Test]
    public void BuildMaximumSpanningTree_ReturnsCorrectEdges()
    {
        var edges = new List<Edge>
        {
            new Edge(1, 2, 10),
            new Edge(2, 3, 5),
            new Edge(1, 3, 1),
        };

        var network = new Network(edges);
        var mst = network.BuildMaximumSpanningTree();

        Assert.That(mst.Count, Is.EqualTo(2));
        Assert.That(mst.Exists(e => e.From == 1 && e.To == 2 && e.Capacity == 10), Is.True);
        Assert.That(mst.Exists(e => e.From == 2 && e.To == 3 && e.Capacity == 5), Is.True);
    }

    [Test]
    public void BuildMaximumSpanningTree_SingleEdge()
    {
        var edges = new List<Edge>
        {
            new Edge(1, 2, 42),
        };

        var network = new Network(edges);
        var mst = network.BuildMaximumSpanningTree();

        Assert.That(mst.Count, Is.EqualTo(1));
        Assert.That(mst[0].Capacity, Is.EqualTo(42));
    }

    [Test]
    public void ParseLineParsesSingleConnection()
    {
        string input = "1: 2 (10)";
        var nodes = Parser.ParseLine(input);

        Assert.That(nodes.Count, Is.EqualTo(1));
        Assert.That(nodes[0].From, Is.EqualTo(1));
        Assert.That(nodes[0].To, Is.EqualTo(2));
        Assert.That(nodes[0].Capacity, Is.EqualTo(10));
    }

    [Test]
    public void ParseLineParsesMultipleConnections()
    {
        string input = "1: 2 (10), 3 (5)";
        var nodes = Parser.ParseLine(input);

        Assert.That(nodes.Count, Is.EqualTo(2));
        Assert.That(nodes.Exists(n => n.From == 1 && n.To == 2 && n.Capacity == 10), Is.True);
        Assert.That(nodes.Exists(n => n.From == 1 && n.To == 3 && n.Capacity == 5), Is.True);
    }

    [Test]
    public void ParseLineParsesLineWithoutConnections()
    {
        string input = "5:";
        var nodes = Parser.ParseLine(input);

        Assert.That(nodes.Count, Is.EqualTo(0));
    }
}
