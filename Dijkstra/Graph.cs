using System.Collections.Generic;

namespace Dijkstra
{
    public class Graph<T>
    {
        public IDictionary<T, Vertex<T>> Vertices { get; private set; }
        public bool IsDirected { get; set; }

        public Graph(bool isDirected = false)
        {
            IsDirected = isDirected;
            Vertices = new Dictionary<T, Vertex<T>>();
        }

        public Graph(Graph<T> other) : this()
        {
            foreach (var v in other.Vertices.Values)
            {
                var v1 = GetOrCreateVertex(v.Key);
                foreach (var vAdj in v.Edges)
                {
                    var v2 = GetOrCreateVertex(vAdj.Key);
                    v1.AddEdge(v2.Key, vAdj.Value);
                }
                Vertices[v1.Key] = v1;
            }
        }

        public Vertex<T> GetOrCreateVertex(T label)
        {
            return GetOrCreateVertex(new Vertex<T>(this, label));
        }

        public Vertex<T> GetOrCreateVertex(Vertex<T> v)
        {
            if (Vertices.ContainsKey(v.Key))
            {
                return Vertices[v.Key];
            }

            var v1 = new Vertex<T>(this, v.Key);
            Vertices[v1.Key] = v1;
            return v1;
        }

        public Vertex<T> Get(T key)
        {
            return Vertices.ContainsKey(key) ? Vertices[key] : null;
        }
    }
}
