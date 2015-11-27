using System.Collections.Generic;

namespace Dijkstra
{
    public static class EnumerableExtensions
    {
        public static Vertex<T> Min<T>(this IEnumerable<Vertex<T>> collection)
        {
            Vertex<T> min = null;
            foreach (var i in collection)
            {
                if (min == null || min.Score > i.Score)
                    min = i;
            }
            return min;
        }
    }
}
