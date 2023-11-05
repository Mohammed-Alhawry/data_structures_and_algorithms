/// <summary>
/// provides generic static helper methods for IComparable types
/// </summary>
public static class Comparable
{
    public static bool IsGreaterThan<T>(T first, T second) where T : IComparable<T>
        => first.CompareTo(second) > 0;

    public static bool IsGreaterOrEqual<T>(T first, T second) where T : IComparable<T>
    => first.CompareTo(second) > 0 || first.CompareTo(second) == 0;

    public static bool IsLessThan<T>(T first, T second) where T : IComparable<T>
        => first.CompareTo(second) < 0;

    public static bool IsLessOrEqual<T>(T first, T second) where T : IComparable<T>
        => first.CompareTo(second) < 0 || first.CompareTo(second) == 0;

}
