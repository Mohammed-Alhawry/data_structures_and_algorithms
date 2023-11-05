
#nullable disable
using System.Security.Cryptography.X509Certificates;

public class WeightedGraph
{
    private readonly Dictionary<string, Node> _nodes = new();
    public int CountOfNodes => _nodes.Count;
    public void AddNode(string str)
    {
        if (_nodes.ContainsKey(str)) return;

        _nodes.Add(str, new Node(str));
    }

    public void AddEdge(string from, string to, double weight)
    {
        var exist = _nodes.ContainsKey(from) && _nodes.ContainsKey(to);
        if (!exist) return;

        var fromNode = _nodes[from];
        var toNode = _nodes[to];
        fromNode.AddEdge(toNode, weight);
        toNode.AddEdge(fromNode, weight);
    }


    private void AddEdge(Edge edge)
    {
        var fromNode = _nodes[edge.From.Label];
        fromNode.AddEdge(edge.To, edge.Weight);
    }
    public bool ContainsNode(string label)
    {
        return _nodes.ContainsKey(label);
    }
    public WeightedGraph GetMinimumSpanningTree()
    {

        var tree = new WeightedGraph();
        if (tree.CountOfNodes == 0)
            return tree;

        var edges = new PriorityQueue<Edge, double>();
        
        var startNode = _nodes.FirstOrDefault().Value;        
        tree.AddNode(startNode.Label);
        foreach (var edge in startNode.GetEdges())
            edges.Enqueue(edge, edge.Weight);


        while (tree.CountOfNodes != this.CountOfNodes && edges.Count != 0)
        {
            var minEdge = edges.Dequeue();
            var nextNode = minEdge.To;
            if (tree.ContainsNode(nextNode.Label))
                continue;

            tree.AddNode(nextNode.Label);
            tree.AddEdge(minEdge.From.Label, nextNode.Label, minEdge.Weight);

            foreach (var edge in nextNode.GetEdges())
            {
                if (!tree.ContainsNode(edge.To.Label))
                    edges.Enqueue(edge, edge.Weight);
            }
        }
        return tree;
    }


    public bool HasCycle()
    {
        var visited = new HashSet<Node>();

        foreach (var node in _nodes.Values)
        {
            if (!visited.Contains(node) && HasCycle(node, null, visited))
                return true;
        }

        return false;
    }

    private bool HasCycle(Node node, Node parent, HashSet<Node> visited)
    {
        visited.Add(node);
        foreach (var edge in node.GetEdges())
        {
            if (edge.To == parent) continue;
            if (visited.Contains(edge.To))
                return true;

            return HasCycle(edge.To, node, visited);
        }

        return false;
    }

    public string GetShortestPath(string from, string to)
    {
        var exists = _nodes.ContainsKey(from) && _nodes.ContainsKey(to);
        if (!exists) return "";

        var fromNode = _nodes[from];

        var previousNodes = new Dictionary<Node, Node>();
        previousNodes[fromNode] = null;

        var distances = new Dictionary<Node, double>();
        foreach (var node in _nodes.Values)
            distances[node] = double.MaxValue;
        distances[fromNode] = 0;

        var queue = new PriorityQueue<Node, double>();
        queue.Enqueue(fromNode, 0);

        var visited = new HashSet<Node>();
        while (queue.Count != 0)
        {
            var current = queue.Dequeue();
            visited.Add(current);

            foreach (var edge in current.GetEdges())
            {
                var neighbor = edge.To;
                if (visited.Contains(neighbor)) continue;
                var newDistance = edge.Weight + distances[current];
                if (newDistance < distances[neighbor])
                {
                    distances[neighbor] = newDistance;
                    previousNodes[neighbor] = current;
                    queue.Enqueue(neighbor, distances[neighbor]);
                }
            }
        }

        return BuildPath(_nodes[to], previousNodes, separator: "->");
    }

    private string BuildPath(Node toNode, Dictionary<Node, Node> parents, string separator)
    {
        var stack = new Stack<Node>();
        var previous = toNode;
        while (previous is not null)
        {
            stack.Push(previous);
            previous = parents.GetValueOrDefault(previous);
        }

        return string.Join(separator, stack);
    }

    public void Print()
    {
        foreach (var node in _nodes.Values)
        {
            Console.Write($"{node} is connected with [");
            foreach (var n in node.GetEdges())
            {
                Console.Write($"{n}, ");
            }
            Console.WriteLine(']');
        }
    }

    private class Edge
    {
        public Node From { get; }
        public Node To { get; }
        public double Weight { get; }
        public Edge(Node from, Node to, double weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }

        public override string ToString()
        {
            return From + "->" + To;
        }
        public override bool Equals(object obj)
        {
            if (obj is null || obj is not Edge) return false;
            var casting = obj as Edge;
            return (casting.From == this.From && casting.To == this.To);
        }

        public override int GetHashCode()
        {
            return From.GetHashCode() + To.GetHashCode();
        }
    }

    private class Node
    {
        private readonly HashSet<Edge> _edges = new();

        public string Label { get; }

        public IEnumerable<Edge> GetEdges() => _edges;

        public Node(string label)
        {
            Label = label;
        }

        public void AddEdge(Node to, double weight)
        {
            _edges.Add(new Edge(this, to, weight));
        }

        public override string ToString()
        {
            return Label;
        }

        public override bool Equals(object obj)
        {
            if (obj is null || obj is not Node) return false;
            var casting = obj as Node;
            return casting.Label == this.Label;
        }

        public override int GetHashCode()
        {
            return Label.GetHashCode();
        }
    }
}