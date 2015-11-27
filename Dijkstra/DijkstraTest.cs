using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Dijkstra
{
    [TestFixture]
    public class DijkstraTest
    {
        private Graph<int> _graph;

        public Graph<int> Graph
        {
            get { return _graph ?? (_graph = WeightedGraphBuilder.FromFile("Dijkstra.txt", false)); }
        }

        [Test]
        public void ParseFromFile()
        {
            Console.WriteLine("Vertices: {0}", Graph.Vertices.Count);
        }

        [Test]
        public void TestSortedSet()
        {
            var set = new SortedSet<int>();
            for (int i = 0; i < 10; i++) set.Add(i);
            Assert.IsTrue(set.SequenceEqual(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
        }

        [Test]
        public void TestSmallGraph()
        {
            var g = new Graph<int>();
            for (int i = 0; i < 4; ++i) g.GetOrCreateVertex(i);

            g.Get(0).AddEdge(1, 1);
            g.Get(0).AddEdge(2, 3);
            g.Get(1).AddEdge(2, 1);
            g.Get(1).AddEdge(3, 3);
            g.Get(2).AddEdge(3, 1);

            var algorithm = new Dijkstra<int>(g, 0);
            algorithm.Run();

            Assert.AreEqual(0, g.Get(0).Score);
            Assert.AreEqual(1, g.Get(1).Score);
            Assert.AreEqual(2, g.Get(2).Score);
            Assert.AreEqual(3, g.Get(3).Score);
        }
        
        [Test]
        public void TestAssignment()
        {
            var algorithm = new Dijkstra<int>(Graph, 1);
            algorithm.Run();

            var nodes = new[]
            {
                Graph.Get(7),
                Graph.Get(37),
                Graph.Get(59),
                Graph.Get(82),
                Graph.Get(99),
                Graph.Get(115),
                Graph.Get(133),
                Graph.Get(165),
                Graph.Get(188),
                Graph.Get(197),
            }.ToList();

            nodes.ForEach(n =>
            {
                Console.Write(n.Score);
                Console.Write(",");
            });
        }
    }
}
