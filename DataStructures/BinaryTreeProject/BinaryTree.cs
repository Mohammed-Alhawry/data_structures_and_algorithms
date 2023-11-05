using System.Data;

namespace BinaryTreeProject;
#nullable disable
/// <summary>
/// Represents a binary search tree, note that this class doesn't balance itself,
/// if you need a binary search tree that balance itself use AVLBinaryTree instead
/// </summary>
public class BinaryTree
{
    public Node _root { get; private set; }
    public int Count { get; private set; }
    public void Add(int value)
    {
        var node = new Node(value);
        if (_root is null)
        {
            _root = node;
            Count++;
            return;
        }
        AddNode(node, _root);
    }

    private void AddNode(Node newNode, Node root)
    {
        if (newNode.Value < root.Value)
        {
            if (root.LeftNode is null)
            {
                root.LeftNode = newNode;
                Count++;
                return;
            }

            AddNode(newNode, root.LeftNode);
        }

        else if (newNode.Value > root.Value)
        {
            if (root.RightNode is null)
            {
                root.RightNode = newNode;
                Count++;
                return;
            }
            AddNode(newNode, root.RightNode);
        }

        else
            return;
    }
    public int CountLeaves()
    {
        return CountLeaves(_root);
    }
    public int Max()
    {
        return Max(_root);
    }

    public bool Contains(int value)
    {
        return Contains(value, _root);
    }

    private bool Contains(int value, Node root)
    {
        if (root is null) return false;
        if (root.Value == value) return true;

        return Contains(value, root.LeftNode)
            || Contains(value, root.RightNode);
    }

    private int Max(Node root)
    {
        if (root is null) return 0;
        if (root.RightNode is null) return root.Value;
        return Max(root.RightNode);
    }
    private int CountLeaves(Node root)
    {
        if (root is null) return 0;
        if (IsLeaf(root)) return 1;

        return CountLeaves(root.LeftNode) + CountLeaves(root.RightNode);
    }
    public bool Find(int value)
    {
        var traverser = _root;
        while (traverser is not null)
        {
            if (value < traverser.Value)
                traverser = traverser.LeftNode;
            else if (value > traverser.Value)
                traverser = traverser.RightNode;
            else
                return true;
        }

        return false;
    }

    public void TraverseUsingPreOrder()
    {
        TraverseUsingPreOrder(_root);
    }
    private void TraverseUsingPreOrder(Node root)
    {
        if (root is null)
            return;
        Console.WriteLine(root.Value);
        TraverseUsingPreOrder(root.LeftNode);
        TraverseUsingPreOrder(root.RightNode);
    }
    public void TraverseUsingInOrder()
    {
        TraverseUsingInOrder(_root);
    }
    public void TraverseUsingPostOrder()
    {
        TraverseUsingPostOrder(_root);
    }

    private void TraverseUsingInOrder(Node root)
    {
        if (root is null)
            return;

        TraverseUsingInOrder(root.LeftNode);
        Console.WriteLine(root.Value);
        TraverseUsingInOrder(root.RightNode);
    }
    private void TraverseUsingPostOrder(Node root)
    {
        if (root is null)
            return;

        TraverseUsingPostOrder(root.LeftNode);
        TraverseUsingPostOrder(root.RightNode);
        Console.WriteLine(root.Value);
    }

    public bool IsBalanced() => IsBalanced(_root);
    private bool IsBalanced(Node root)
    {
        if (root is null)
            return true;

        var rightHeight = Height(root.RightNode) == -1 ? 0 : Height(root.RightNode) + 1;
        var leftHeight = Height(root.LeftNode) == -1 ? 0 : Height(root.RightNode) + 1;
        if (Difference(rightHeight, leftHeight) > 1)
            return false;
        return true;
    }
    private int Difference(int first, int last)
    {
        return (first > last) ? first - last : last - first;
    }
    private int Height(Node node)
    {
        if (node is null)
            return -1;

        if (IsLeaf(node))
            return 0;

        return 1 + Math.Max(Height(node.LeftNode), Height(node.RightNode));
    }

    private static bool IsLeaf(Node node)
    {
        return node.LeftNode is null && node.RightNode is null;
    }

    // O(log n)
    public int MinValue()
    {
        if (_root is null)
            throw new EmptyBinaryTreeException();

        var current = _root;
        while (current.LeftNode is not null)
            current = current.LeftNode;

        return current.Value;
    }

    public bool Equals(BinaryTree outTree)
    {
        if (outTree is null)
            return false;

        return NodesEqual(outTree._root, _root);
    }

    public static bool IsBinarySearchTree(BinaryTree tree)
    {
        if (tree is null) throw new ArgumentNullException();
        return IsValueInCorrectRange(tree._root, int.MinValue, int.MaxValue);
    }

    public bool IsPerfect()
    {
        return IsPerfect(_root);
    }

    private bool IsPerfect(Node root)
    {
        return Count == (Math.Pow(2, Height() + 1) - 1);
    }

    public void PrintNodesAtDistance(int distance)
    {
        PrintNodesAtDistance(distance, _root);
    }
    public IEnumerable<int> GetNodesAtDistance(int distance)
    {
        return GetNodesAtDistance(distance, _root, new List<int>());
    }

    private IEnumerable<int> GetNodesAtDistance(int distance, Node root, List<int> list)
    {
        if (root is null) return list;

        if (distance == 0)
        {
            list.Add(root.Value);
            return list;
        }

        GetNodesAtDistance(distance - 1, root.LeftNode, list);
        GetNodesAtDistance(distance - 1, root.RightNode, list);
        return list;
    }

    public void TraverseUsingLevelOrder()
    {
        for (int i = 0; i <= Height(); i++)
        {
            foreach (var item in GetNodesAtDistance(i))
                Console.WriteLine(item);
        }
    }
    private void PrintNodesAtDistance(int distance, Node root)
    {
        if (distance < 0) throw new ArgumentOutOfRangeException();

        if (root is null)
            return;

        if (distance == 0)
        {
            Console.WriteLine(root.Value);
            return;
        }

        PrintNodesAtDistance(distance - 1, root.LeftNode);
        PrintNodesAtDistance(distance - 1, root.RightNode);
    }
    private static bool IsValueInCorrectRange(Node root, int min, int max)
    {
        if (root is null) return true;

        if (root.Value < min || root.Value > max)
            return false;

        return IsValueInCorrectRange(root.LeftNode, int.MinValue, root.Value)
               && IsValueInCorrectRange(root.RightNode, root.Value, int.MaxValue);
    }

    private bool NodesEqual(Node first, Node second)
    {
        if (first is null && second is null) return true;

        if (first is not null && second is not null)
            return first.Value == second.Value && NodesEqual(first.LeftNode, second.LeftNode)
                 && NodesEqual(first.RightNode, second.RightNode);

        return false;
    }

    // O(n) this method assume that the tree is not sorted     
    private int MinValue(Node node)
    {
        if (node is null)
            return int.MaxValue;

        if (IsLeaf(node))
            return node.Value;

        var leftValue = MinValue(node.LeftNode);
        var rightValue = MinValue(node.RightNode);

        return Math.Min(Math.Min(leftValue, rightValue), node.Value);
    }
    public int Height()
    {
        return Height(_root);
    }

    public IEnumerable<int> GetAncestors(int value)
    {
        var list = new List<int>();
        GetAncestors(value, _root, list);
        return list;
    }

    private bool GetAncestors(int value, Node root, List<int> list)
    {
        if (root is null) return false;
        if (root.Value == value)
            return true;

        if (GetAncestors(value, root.LeftNode, list)
        || GetAncestors(value, root.RightNode, list))
        {
            list.Add(root.Value);
            return true;
        }

        return false;

    }
    public bool AreSibling(int first, int second)
    {
        return AreSibling(_root, first, second);
    }

    private bool AreSibling(Node root, int first, int second)
    {
        if (root is null) return false;

        var areSibling = false;
        if (root.LeftNode != null && root.RightNode != null)
        {
            areSibling = (root.LeftNode.Value == first && root.RightNode.Value == second) ||
                         (root.RightNode.Value == first && root.LeftNode.Value == second);
        }

        return areSibling
                || AreSibling(root.LeftNode, first, second)
                || AreSibling(root.RightNode, first, second);

    }
    public class Node
    {
        public int Value { get; }
        public Node RightNode { get; set; }
        public Node LeftNode { get; set; }
        public Node(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"Node={Value}";
        }

    }
}
