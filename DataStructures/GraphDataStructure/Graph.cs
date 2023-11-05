#nullable disable
public class Graph
{
    private readonly Dictionary<Node, LinkedList<Node>> _adjacencyList = new();
    private readonly Dictionary<string, Node> _nodes = new();

    public void AddNode(string label)
    {
        var node = new Node(label);
        _nodes.TryAdd(label, node);
        _adjacencyList.TryAdd(node, new LinkedList<Node>());
    }

    public void AddEdge(string from, string to)
    {
        var exists = _nodes.ContainsKey(from) && _nodes.ContainsKey(to);
        if (!exists)
            throw new InvalidOperationException();

        var fromNode = _nodes[from];
        var toNode = _nodes[to];

        var toNodeExists = _adjacencyList[fromNode].Contains(toNode);
        if (!toNodeExists)
            _adjacencyList[fromNode].AddLast(toNode);
    }

    public void Print()
    {
        foreach (var node in _adjacencyList.Keys)
        {
            Console.Write($"{node} is connected with [");
            foreach (var n in _adjacencyList[node])
            {
                System.Console.Write($"{n}, ");
            }
            System.Console.WriteLine(']');
        }
    }


    public bool HasCycle()
    {
        var all = new HashSet<Node>(_nodes.Values);
        var visited = new HashSet<Node>();
        var visiting = new HashSet<Node>();

        while (all.Count != 0)
        {
            if (HasCycle(all.First(), all, visiting, visited))
            {
                return true;
            }
        }

        return false;
    }

    

    private bool HasCycle(Node node, HashSet<Node> all, HashSet<Node> visiting, HashSet<Node> visited)
    {
        all.Remove(node);
        visiting.Add(node);

        foreach (var item in _adjacencyList[node])
        {
            if (visited.Contains(item))
                continue;

            if (visiting.Contains(item))
            {
                return true;
            }

            if (HasCycle(item, all, visiting, visited))
            {
                return true;
            }
        }

        visiting.Remove(node);
        visited.Add(node);

        return false;
    }
    public IEnumerable<string> TopologicalSort()
    {
        if (HasCycle())
            throw new GraphHasCycleException();
        var visited = new HashSet<Node>();
        var stack = new Stack<Node>();

        foreach (var node in _nodes.Values)
            TopologicalSort(node, visited, stack);

        return stack.Select(e => e.Label);
    }

    private void TopologicalSort(Node node, HashSet<Node> visited, Stack<Node> stack)
    {
        if (visited.Contains(node)) return;

        visited.Add(node);

        foreach (var item in _adjacencyList[node])
            TopologicalSort(item, visited, stack);


        stack.Push(node);
    }

    public void TraverseBreadthFirst(string label)
    {
        if (label is null || !_nodes.ContainsKey(label)) return;

        var queue = new Queue<Node>();
        var visitedNodes = new HashSet<Node>();
        queue.Enqueue(_nodes[label]);

        while (queue.Count != 0)
        {
            var current = queue.Dequeue();

            if (visitedNodes.Contains(current))
                continue;

            Console.WriteLine(current);
            visitedNodes.Add(current);

            foreach (var item in _adjacencyList[current])
            {
                if (!visitedNodes.Contains(item))
                    queue.Enqueue(item);
            }
        }
    }

    public void TraverseDepthFirstUsingStack(string label)
    {
        if (label is null || !_nodes.ContainsKey(label)) return;

        var node = _nodes[label];
        var stack = new Stack<Node>();
        var visited = new HashSet<Node>();
        stack.Push(node);
        while (stack.Count != 0)
        {
            var current = stack.Pop();
            if (!visited.Add(current))
                continue;

            Console.WriteLine(current);
            foreach (var item in _adjacencyList[current])
                stack.Push(item);
        }
    }

    public void TraverseDepthFirstUsingRecursion(string label)
    {
        if (label is null) return;

        if (_nodes.ContainsKey(label))
            TraverseDepthFirstUsingRecursion(_nodes[label], new HashSet<Node>());
    }


    private void TraverseDepthFirstUsingRecursion(Node node, HashSet<Node> visitedNodes)
    {
        System.Console.WriteLine(node);
        visitedNodes.Add(node);
        foreach (var n in _adjacencyList[node])
        {
            if (!visitedNodes.Contains(n))
                TraverseDepthFirstUsingRecursion(n, visitedNodes);
        }

    }
    public void RemoveNode(string label)
    {
        var exists = _nodes.ContainsKey(label);
        var node = new Node(label);
        if (exists)
        {
            foreach (var n in _adjacencyList.Keys)
                _adjacencyList[n].Remove(node);

            _nodes.Remove(label);
            _adjacencyList.Remove(node);
        }
    }

    public void RemoveEdge(string from, string to)
    {
        var exists = _nodes.ContainsKey(from) && _nodes.ContainsKey(to);

        if (exists)
            _adjacencyList[new Node(from)].Remove(new Node(to));
    }

    private class Node
    {
        public string Label { get; }

        public Node(string label)
        {
            Label = label;
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
