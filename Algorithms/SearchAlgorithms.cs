public class SearchAlgorithms
{
    public static int LinearSearch(int[] arr, int item)
    {
        for (var i = 0; i < arr.Length; i++)
        {
            if (arr[i] == item)
                return i;
        }

        return -1;
    }

    public static int BinarySearchUsingIteration(int[] arr, int item)
    {
        var startIndex = 0;
        var lastIndex = arr.Length - 1;
        int middle;

        while (lastIndex >= startIndex)
        {
            middle = (startIndex + lastIndex) / 2;
            if (arr[middle] == item)
                return middle;

            if (item < arr[middle])
                lastIndex = middle - 1;

            else
                startIndex = middle + 1;

        }
        return -1;
    }

    public static int JumpSearch(int[] arr, int item)
    {
        var blockSize = (int)Math.Sqrt(arr.Length);
        var first = 0;
        var next = blockSize;

        while (first < arr.Length && arr[next - 1] < item)
        {
            first = next;
            next += blockSize;
            if (next > arr.Length)
                next = arr.Length;
        }

        for (int i = first; i < next; i++)
            if (arr[i] == item)
                return i;

        return -1;
    }

    public static int ExponentialSearch(int[] arr, int item)
    {
        if (arr is null || arr.Length == 0)
            return -1;
        
        var bound = 1;
        while (bound < arr.Length && arr[bound] < item)
        {
            bound *= 2;
        }

        var startIndex = bound / 2;
        var lastIndex = Math.Min(bound, arr.Length - 1);

        
        return BinarySearchUsingRecursion(arr, item, startIndex, lastIndex);
    }

    public static int TernarySearchUsingIteration(int[] arr, int item)
    {
        var startIndex = 0;
        var lastIndex = arr.Length - 1;

        while (lastIndex >= startIndex)
        {
            var partitionSize = (lastIndex - startIndex) / 3;
            int mid1 = startIndex + partitionSize;
            int mid2 = lastIndex - partitionSize;

            if (arr[mid1] == item)
                return mid1;
            if (arr[mid2] == item)
                return mid2;

            if (item < arr[mid1])
                lastIndex = mid1 - 1;

            else if (item > arr[mid2])
                startIndex = mid2 + 1;

            else
            {
                startIndex = mid1 + 1;
                lastIndex = mid2 - 1;
            }
        }
        return -1;
    }

    public static int TernarySearchUsingRecursion(int[] arr, int item)
    {
        return TernarySearchUsingRecursion(arr, item, 0, arr.Length - 1);
    }

    private static int TernarySearchUsingRecursion(int[] arr, int item, int startIndex, int lastIndex)
    {
        if (lastIndex < startIndex)
            return -1;

        var partitionSize = (lastIndex - startIndex) / 3;
        int mid1 = startIndex + partitionSize;
        int mid2 = lastIndex - partitionSize;

        if (arr[mid1] == item)
            return mid1;
        if (arr[mid2] == item)
            return mid2;


        if (item < arr[mid1])
            lastIndex = mid1 - 1;
        else if (item > arr[mid2])
        {
            startIndex = mid2 + 1;
        }

        else
        {
            startIndex = mid1 + 1;
            lastIndex = mid2 - 1;
        }

        return TernarySearchUsingRecursion(arr, item, startIndex, lastIndex);
    }

    public static int BinarySearchUsingRecursion(int[] arr, int item, int startIndex, int lastIndex)
    {
        if (lastIndex < startIndex)
            return -1;

        var middle = (startIndex + lastIndex) / 2;

        if (arr[middle] == item)
            return middle;

        else if (item < arr[middle])
            return BinarySearchUsingRecursion(arr, item, startIndex, middle - 1);

        else
            return BinarySearchUsingRecursion(arr, item, middle + 1, lastIndex);


    }
}
