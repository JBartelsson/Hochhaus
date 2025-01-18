using System.Collections.Generic;
using System.Text;

public static class ListExtensions
{
    public static string ToFormattedString<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
            return "[]"; // Empty list representation

        StringBuilder sb = new StringBuilder();
        sb.Append("[");

        for (int i = 0; i < list.Count; i++)
        {
            sb.Append(list[i]?.ToString()); // Call ToString() on each element
            if (i < list.Count - 1)
                sb.Append(", ");
        }

        sb.Append("]");
        return sb.ToString();
    }
}