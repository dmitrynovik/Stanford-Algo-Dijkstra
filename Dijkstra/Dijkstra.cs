using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Dijkstra
{
    public class Dijkstra<T>
    {
        private readonly Graph<T> _graph;
        private readonly Vertex<T> _source;
 
        public Dijkstra(Graph<T> graph, T source)
        {
            _graph = graph;
            _source = graph.Get(source);
            if (_source == null) throw new ArgumentException(string.Format("Vertex {0} not found", source));
        }

        public void Run()
        {
            // setup:
            _source.Score = 0;
            var unexplored = GetAllNodes();
            var edges = GetSortedEdges();
            var explored = new List<Vertex<T>> { _source };
            unexplored.Remove(_source);

            // main loop:
            while (unexplored.Any() && edges.Any())
            {
                var least = edges.Min();
                edges.Remove(least);
                if (!_graph.IsDirected) edges.Remove(new Edge<T>(least.To, least.From, least.Weight));

                if (explored.Contains(least.From) && !explored.Contains(least.To))
                {
                    least.To.Score = least.From.Score + least.Weight;
                    foreach (var edge in least.To.Edges)
                    {
                        var v = _graph.Get(edge.Key);
                        int edgeWeight = edge.Value;
                        if (v.Score > least.To.Score + edgeWeight)
                            v.Score = least.To.Score + edgeWeight;
                    }
                    unexplored.Remove(least.To);
                    explored.Add(least.To);
                }
            }
        }

        private ICollection<Vertex<T>> GetAllNodes()
        {
            return _graph.Vertices.Values.ToList();
        }

        private ICollection<Edge<T>> GetSortedEdges()
        {
            var result = new List<Edge<T>>();
            int i = 0;
            foreach (var v1 in _graph.Vertices.Values)
            {
                foreach (var v2 in v1.Edges)
                {
                    var e = new Edge<T>(v1, _graph.Get(v2.Key), v2.Value);
                    result.Add(e);
                    i++;
                }
            }
            Console.WriteLine("Must be {0} edges", i);
            return result;
        }
    }
}
