namespace BinaryTreeProject;
#nullable disable
/// <summary>
/// Represents an ordered AVL Binary Search Tree that balance itself automatically
/// </summary>
public class AVLBinaryTree<T> where T : IComparable<T>
{
    private AVLNode _root;
    public int Height => _root.Height;
    public int Count { get; private set; }
    public void Add(T value)
    {
        if (value is null) throw new ArgumentNullException();

        _root = Add(value, _root);
    }

    public T MinValue()
    {
        if (_root is null)
            throw new EmptyBinaryTreeException();

        var current = _root;
        while (current.LeftChild is not null)
            current = current.LeftChild;

        return current.Value;
    }

    public T MaxValue()
    {
        if (_root is null)
            throw new EmptyBinaryTreeException();

        var current = _root;
        while (current.RightChild is not null)
            current = current.RightChild;

        return current.Value;
    }

    public bool IsPerfect()
    => Count == (Math.Pow(2, Height + 1) - 1);

    public IEnumerable<T> GetAncestors(T value)
    {
        var list = new List<T>();
        GetAncestors(value, _root, list);
        return list;
    }

    public bool AreSibling(T first, T second)
    => AreSibling(_root, first, second);


    /// <summary>
    /// Gets items from left to right at the specified depth
    /// </summary>
    public IEnumerable<T> GetItemsAtDepth(int depth)
    => GetItemsFromLeftToRightAtDepth(depth, _root, new List<T>());


    ////////////////////// Private Helper Methods /////////////////////////////////////////

    private bool GetAncestors(T value, AVLNode root, List<T> list)
    {
        if (root is null) return false;
        if (root.Value.Equals(value))
            return true;

        if (GetAncestors(value, root.LeftChild, list)
        || GetAncestors(value, root.RightChild, list))
        {
            list.Add(root.Value);
            return true;
        }

        return false;
    }

    private bool AreSibling(AVLNode root, T first, T second)
    {
        if (root is null) return false;

        var areSibling = false;
        if (root.LeftChild != null && root.RightChild != null)
        {
            areSibling = (root.LeftChild.Value.Equals(first) && root.RightChild.Value.Equals(second)) ||
                         (root.RightChild.Value.Equals(first) && root.LeftChild.Value.Equals(second));
        }

        return areSibling
                || AreSibling(root.LeftChild, first, second)
                || AreSibling(root.RightChild, first, second);

    }

    private IEnumerable<T> GetItemsFromLeftToRightAtDepth(int depth, AVLNode root, List<T> list)
    {
        if (root is null)
            return list;
        if (depth == 0)
        {
            list.Add(root.Value);
            return list;
        }
        GetItemsFromLeftToRightAtDepth(depth - 1, root.LeftChild, list);
        GetItemsFromLeftToRightAtDepth(depth - 1, root.RightChild, list);
        return list;
    }

    private AVLNode Add(T value, AVLNode root)
    {
        if (root is null)
        {
            Count++;
            return new AVLNode(value);
        }

        if (Comparable.IsLessThan(value, root.Value))
            root.LeftChild = Add(value, root.LeftChild);

        else if (Comparable.IsGreaterThan(value, root.Value))
            root.RightChild = Add(value, root.RightChild);

        SetHeight(root);
        return Balance(root);
    }

    private AVLNode RotateLeft(AVLNode root)
    {
        var newRoot = root.RightChild;

        root.RightChild = newRoot.LeftChild;
        newRoot.LeftChild = root;

        SetHeight(root);
        SetHeight(newRoot);

        return newRoot;
    }

    private AVLNode RotateRight(AVLNode root)
    {
        var newRoot = root.LeftChild;

        root.LeftChild = newRoot.RightChild;
        newRoot.RightChild = root;

        SetHeight(root);
        SetHeight(newRoot);

        return newRoot;
    }

    private AVLNode Balance(AVLNode root)
    {
        if (IsRightHeavy(root))
        {
            if (GetBalanceFactor(root.RightChild) > 0)
                root.RightChild = RotateRight(root.RightChild);
            return RotateLeft(root);
        }

        else if (IsLeftHeavy(root))
        {
            if (GetBalanceFactor(root.LeftChild) < 0)
                root.LeftChild = RotateLeft(root.LeftChild);
            return RotateRight(root);
        }

        else
            return root;
    }

    private void SetHeight(AVLNode node)
    => node.Height = 1 + Math.Max(GetHeight(node.LeftChild), GetHeight(node.RightChild));

    private int GetHeight(AVLNode root)
    => root?.Height ?? -1;

    private int GetBalanceFactor(AVLNode root)
    => root is null ? 0 : GetHeight(root.LeftChild) - GetHeight(root.RightChild);

    private bool IsLeftHeavy(AVLNode root)
    => GetBalanceFactor(root) > 1;

    private bool IsRightHeavy(AVLNode root)
    => GetBalanceFactor(root) < -1;

    private class AVLNode
    {
        public int Height { get; set; }
        public AVLNode RightChild { get; set; }
        public AVLNode LeftChild { get; set; }
        public T Value { get; }

        public AVLNode(T value)
        {
            Value = value;
        }
        public override string ToString()
        {
            return $"Value={Value}";
        }
    }
}

