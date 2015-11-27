using System;
using System.Collections.Generic;

namespace Dijkstra
{
    public class Vertex<T> : IComparable<Vertex<T>>, IComparable
    {
        public const int Infinity = 1000000;

        private readonly Graph<T> _g; 
        public T Key { get; set; }
        public bool IsExplored { get; set; }

        public IDictionary<T, int> Edges { get; private set; }

        public Vertex(Graph<T> g)
        {
            _g = g;
            Edges = new Dictionary<T, int>();
        }

        public Vertex(Graph<T> g, T key) : this(g)
        {
            Key = key;
            Score = Infinity;
        }

        public Vertex<T> Get(T key)
        {
            return _g.Get(key);
        }

        public int Score { get; set; }

        public void AddEdge(T key, int weight)
        {
            Edges[key] = weight;
            if (!_g.IsDirected)
            {
                var v = _g.Get(key);
                v.Edges[this.Key] = weight;
            }
        }

        public override int GetHashCode()
        {
            return Key == null ? 0 : Key.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            var other = obj as Vertex<T>;
            return other == null ? 1 : CompareTo(other);
        }

        public int CompareTo(Vertex<T> other)
        {
            return other == null ? 1 : Score.CompareTo(other.Score);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Vertex<T>;
            return other != null && other.Key.Equals(Key);
        }

        public override string ToString()
        {
            return Key.ToString();
        }
    }
}
