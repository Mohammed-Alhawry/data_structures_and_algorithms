
using System.Globalization;
using System.Text.RegularExpressions;

public class Sort
{
    public static void AscendingBubbleSort(int[] ints)
    {
        if (ints is null || ints.Length < 2) return;

        bool isSorted;
        for (int i = 0; i < ints.Length; i++)
        {
            isSorted = true;
            for (int j = 1; j < ints.Length - i; j++)
            {
                var first = j - 1;
                var second = j;
                if (ints[first] > ints[second])
                {
                    Swap(ints, first, second);
                    isSorted = false;
                }
            }

            if (isSorted)
                break;
        }
    }

    public static void AscendingSelectionSort(int[] ints)
    {
        for (var i = 0; i < ints.Length; i++)
        {
            int smallestIndex = GetIndexOfSmallestItem(ints, i);
            Swap(ints, smallestIndex, i);
        }
    }

    public static void AscendingInsertionSort(int[] ints)
    {
        for (int i = 1; i < ints.Length; i++)
        {
            var current = ints[i];
            var j = i - 1;
            while (j >= 0 && current < ints[j])
            {
                ints[j + 1] = ints[j];
                j--;
            }
            ints[j + 1] = current;
        }
    }

    public static void MergeSort(int[] arr)
    {
        if (arr.Length < 2)
            return;

        var middle = arr.Length / 2;

        var leftArr = new int[middle];
        for (var f = 0; f < middle; f++)
            leftArr[f] = arr[f];

        var rightArr = new int[arr.Length - middle];
        for (int s = middle; s < arr.Length; s++)
            rightArr[s - middle] = arr[s];

        MergeSort(leftArr);
        MergeSort(rightArr);

        MergeInSortedOrder(arr, leftArr, rightArr);
    }

    public static void QuickSort(int[] arr)
    {
        QuickSort(arr, 0, arr.Length - 1);
    }

    public static void CountingSort(int[] arr)
    {
        if (arr is null || arr.Length < 2)
            return;

        var max = arr[0];
        foreach (var item in arr)
            if (item > max)
                max = item;

        var counts = new int[max + 1];
        foreach (var item in arr)
            counts[item]++;

        var k = 0;
        for (var i = 0; i < counts.Length; i++)
            for (int j = 0; j < counts[i]; j++)
                arr[k++] = i;
    }

    public static void BucketSort(int[] arr)
    {
        var buckets = new LinkedList<int>[3];
        for (int i = 0; i < buckets.Length; i++)
            buckets[i] = new LinkedList<int>();

        foreach (var i in arr)
        {
            var position = i / buckets.Length;
            while (position >= buckets.Length) // the catch is here(the bug)
                position /= buckets.Length;

            buckets[position].AddLast(i);
        }
        
        var j = 0;
        for (int i = 0; i < buckets.Length; i++)
        {
            var bucket = buckets[i].ToArray();
            CountingSort(bucket);
            foreach (var item in bucket)
                arr[j++] = item;
        }
    }

    private static void QuickSort(int[] arr, int startIndex, int lastIndex)
    {
        if (startIndex >= lastIndex)
            return;
        int boundary = Partition(arr, startIndex, lastIndex);

        QuickSort(arr, startIndex, boundary - 1);
        QuickSort(arr, boundary + 1, lastIndex);

    }

    private static int Partition(int[] arr, int startIndex, int lastIndex)
    {
        var pivot = arr[lastIndex];
        var boundary = startIndex - 1;

        for (int i = startIndex; i <= lastIndex; i++)
        {
            if (arr[i] <= pivot)
            {
                boundary++;
                Swap(arr, boundary, i);
            }
        }

        return boundary;
    }

    private static void MergeInSortedOrder(int[] arr, int[] leftArr, int[] rightArr)
    {
        var leftEnd = 0;
        var rightEnd = 0;
        var i = 0;
        while (leftEnd < leftArr.Length && rightEnd < rightArr.Length)
        {
            if (leftArr[leftEnd] <= rightArr[rightEnd])
                arr[i++] = leftArr[leftEnd++];
            else
                arr[i++] = rightArr[rightEnd++];
        }

        while (leftEnd < leftArr.Length)
            arr[i++] = leftArr[leftEnd++];

        while (rightEnd < rightArr.Length)
            arr[i++] = rightArr[rightEnd++];
    }

    private static void ShiftItemsToRightByOne(int[] ints, int startIndex, int endIndex)
    {
        for (int i = endIndex; i > startIndex; i--)
        {
            ints[endIndex] = ints[--endIndex];
        }
    }

    private static int GetIndexOfSmallestItem(int[] ints, int startIndex)
    {
        var indexOfSmallestItem = startIndex;
        for (int i = startIndex; i < ints.Length; i++)
        {
            if (ints[i] < ints[indexOfSmallestItem])
                indexOfSmallestItem = i;
        }

        return indexOfSmallestItem;
    }

    private static void Swap(int[] ints, int index1, int index2)
    {
        (ints[index1], ints[index2]) = (ints[index2], ints[index1]);
    }
}