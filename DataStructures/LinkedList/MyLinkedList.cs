public class MyLinkedList<T>
{
    private Node? _first;
    private Node? _last;


    public bool IsEmpty() => _first is null;
    public int Count { get; private set; } = 0;
    public void AddFirst(T item)
    {
        var newNode = new Node(item);
        if (IsEmpty())
        {
            _first = _last = newNode;
        }

        else
        {
            newNode.Next = _first;
            _first = newNode;
        }

        Count++;
    }

    public void AddLast(T item)
    {
        Node newNode = new Node(item);
        if (IsEmpty())
        {
            _first = _last = newNode;
        }
        else
        {
            _last.Next = newNode;
            _last = newNode;
            _last.Next = _first;
        }

        Count++;
    }

    public void DeleteFirst()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("the linkedList is already Empty");
        }
        else
        {
            if (_first == _last)
            {
                _first = _last = null;
            }
            else
            {
                Node? second = _first.Next;
                _first.Next = null;
                _first = second;
            }

            Count--;
        }
    }

    private Node? GetPrevious(Node node)
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("linkedList is empty");
        }

        var current = _first;
        while (current is not null)
        {
            if (current.Next == node)
            {
                return current;
            }
            current = current.Next;
        }

        return null;

    }
    public void DeleteLast()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("linkedList is already empty");
        }

        else
        {
            if (_first == _last)
            {
                _first = _last = null;
            }
            else
            {

                var beforeLast = GetPrevious(_last);
                _last = beforeLast;
                _last.Next = null;
            }
            Count--;
        }
    }

    public bool Contains(T item)
    {
        return IndexOf(item) != -1;
    }
    public void Print()
    {
        if (Count == 0)
        {
            return;
        }

        Node? traverser = _first;
        while (traverser is not null)
        {
            System.Console.WriteLine(traverser.Value);
            traverser = traverser.Next;
        }
    }

    public int IndexOf(T item)
    {
        if (IsEmpty())
        {
            return -1;
        }

        int index = 0;
        Node traverser = _first;
        while (traverser is not null)
        {
            if (traverser.Value.Equals(item))
            {
                return index;
            }
            traverser = traverser.Next;
            index++;
        }

        return -1;
    }

    public T[] ToArray()
    {


        T[] array = new T[Count];
        int index = 0;
        Node traverser = _first;
        while (traverser is not null)
        {
            array[index++] = traverser.Value!;
            traverser = traverser.Next!;
        }
        return array;
    }

    public void Reverse()
    {
        if (IsEmpty())
        {
            return;
        }

        var previous = _first;
        var current = previous.Next;
        Node? next = null;
        while (current is not null)
        {
            next = current.Next;
            current.Next = previous;
            previous = current;
            current = next;
        }

        var swap = _first;
        _first = _last;
        _last = swap;
        _last.Next = null;
    }

    public T Kth(int numberFromEnd)
    {
        if (IsEmpty())
            throw new InvalidOperationException("linkedList is empty");

        if (numberFromEnd > Count)
            throw new InvalidOperationException("linkedList elements are less than Kth provided number");

        int countToKth = Count - (numberFromEnd - 1);
        var traverser = _first;
        for (int i = 1; i < countToKth; i++)
            traverser = traverser.Next;

        return traverser.Value;

    }

    public T GetKthFromTheEnd(int k)
    {
        if (IsEmpty())
            throw new InvalidOperationException("linkedList is empty");

        int distance = k - 1;
        Node headPointer = _first;
        Node tailPointer = _first;
        for (int i = 1; i <= distance; i++)
        {
            if (headPointer.Next is null)
                throw new InvalidOperationException("Kth is out of linkedList range");

            headPointer = headPointer.Next;
        }

        while (headPointer != _last)
        {
            headPointer = headPointer.Next;
            tailPointer = tailPointer.Next;
        }

        return tailPointer.Value;

    }



    public void PrintMiddle()
    {
        if (IsEmpty())
            throw new InvalidOperationException("linkedList is empty");

        var headPointer = _first.Next;
        var tailPointer = _first;
        var tailPointerCanMove = false;
        var elementsCount = 1;
        while (headPointer is not null)
        {
            elementsCount++;
            headPointer = headPointer.Next;
            if (tailPointerCanMove)
                tailPointer = tailPointer.Next;

            tailPointerCanMove = !tailPointerCanMove;
        }

        Console.WriteLine(tailPointer.Value);
        if (elementsCount % 2 == 0)
            Console.WriteLine(tailPointer.Next.Value);
    }

    public bool HasLoop()
    {
        if (IsEmpty())
            throw new InvalidOperationException("LinkedList is empty");

        var head = _first;
        var tail = _first;
        var count = 1;
        while (head is not null)
        {
            for (int i = 1; i <= count; i++)
            {
                if (head.Next == tail)
                    return true;

                tail = tail.Next;
            }
            tail = _first;
            head = head.Next;
            count++;
        }

        return false;
    }

    public bool HasLoopUsingFloydCycleFindingAlgorithm()
    {
        if (IsEmpty())
            throw new InvalidOperationException("LinkedList is empty");

        var fast = _first;
        var slow = _first;
        while (true)
        {
            for (int i = 1; i <= 2; i++)
            {
                fast = fast.Next;
                if (fast is null)
                    return false;

                if (fast == slow)
                    return true;
            }
            slow = slow.Next;
        }
    }
    class Node
    {
        public T? Value { get; }
        public Node(T value)
        {
            Value = value;
        }
        public Node? Next { get; set; }
    }
}
















