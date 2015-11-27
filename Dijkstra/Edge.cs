using System;
using System.Collections.Generic;

namespace Dijkstra
{
    public class Edge<T> : IComparable, IEquatable<Edge<T>>, IComparable<Edge<T>>
    {
        public Edge(Vertex<T> from, Vertex<T> to, int weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }

        public Vertex<T> From { get; private set; }
        public Vertex<T> To   { get; private set; }
        public int Weight     { get; private set; }
        public int Score { get { return From.Score + Weight; } }

        public override bool Equals(object obj)
        {
            var other = obj as Edge<T>;
            return other != null && other.From == From && other.To == To;
        }

        public bool Equals(Edge<T> other)
        {
            return Equals(From, other.From) && Equals(To, other.To) && Weight == other.Weight;
        }

        public override int GetHashCode()
        {
            long from = From.GetHashCode() * 997;
            long to = To.GetHashCode();
            return (int) ((from + to) % int.MaxValue);
        }

        public int CompareTo(object obj)
        {
            var e = obj as Edge<T>;
            return e == null ? 1 : Score.CompareTo(e.Score);
        }

        public int CompareTo(Edge<T> e)
        {
            return Score.CompareTo(e.Score);
        }

        public override string ToString()
        {
            return string.Format("{0} -> {1} ({2})", From, To, Weight);
        }
    }

    public class EdgeComparer<T> : IComparer<Edge<T>>
    {
        public int Compare(Edge<T> x, Edge<T> y)
        {
            return x.CompareTo(y);
        }
    }
}
