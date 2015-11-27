using System;
using System.IO;
using System.Linq;

namespace Dijkstra
{
    public static class WeightedGraphBuilder
    {
        public static Graph<int> FromFile(string path, bool isDirected)
        {
            var g = new Graph<int>();
            using (var stream = File.OpenRead(path))
            {
                using (var reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = reader.ReadLine() ) != null)
                    {
                        var vertices = line.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
                        if (vertices.Any())
                        {
                            var v = g.GetOrCreateVertex(int.Parse(vertices[0]));
                            foreach (var adj in vertices.Skip(1))
                            {
                                var tokens = adj.Split(',');
                                if (tokens.Length == 2)
                                {
                                    int i2 = int.Parse(tokens[0]);
                                    int weight = int.Parse(tokens[1]);
                                    var v2 = g.GetOrCreateVertex(i2);
                                    v.AddEdge(v2.Key, weight);
                                }
                            }
                        }
                    }
                }
            }
            g.IsDirected = isDirected;
            return g;
        }
    }
}
