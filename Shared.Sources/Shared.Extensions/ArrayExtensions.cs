namespace Shared.Extensions;

public static class ArrayExtensions
{
    public static IEnumerable<T> CSlice<T>(this IEnumerable<T> source, int start, int end)
    {
        if (end > source.Count())
            end = source.Count();

        var copy = new List<T>();

        for (int i = start; i < end; i++)
            copy.Add(source.ElementAt(i));

        return copy.ToArray();
    }

    public static List<T[]> GetPartsBySouce<T>(this T[] source, int size)
    {
        var parts = new List<T[]>();
        var part = new List<T>();

        for (int x = 0; x < source.Length; x++)
        {
            if (part.Count == size) {
                parts.Add(part.ToArray());
                part.Clear();
            }
            
            part.Add(source[x]);
        }

        if (part.Count > 0)
        {
            parts.Add(part.ToArray());
            part.Clear();
        }
        
        return parts;
    }
}