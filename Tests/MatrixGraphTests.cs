using System;
using System.Collections.Generic;
using Domain.Entities;
using NUnit.Framework;

namespace Tests
{
    public class MatrixGraphTests
    {

        private OrientedGraph _graph = new OrientedGraph(new List<Relation>
        {
            new Relation(1, 2),
            new Relation(1, 3),
            new Relation(1, 4),
            new Relation(3, 2)
        });
        
        private OrientedGraph _notCoupledGraph = new OrientedGraph(new List<Relation>
        {
            new Relation(1, 2),
            new Relation(1, 3),
            new Relation(1, 4),
            new Relation(3, 2),
            new Relation(6, 7),
        });
        
        private OrientedGraph _notCoupledGraph2 = new OrientedGraph(new List<Relation>
        {
            new Relation(1, 2),
            new Relation(1, 3),
            new Relation(1, 4),
            new Relation(3, 2),
            new Relation(6, 7),
            new Relation(6, 5),
        });
        
        [Test]
        public void AdjacencyMatrix()
        {
            Assert.AreEqual(new[]
            {
                new [] {0, 1, 1, 1},
                new [] {0, 0, 0, 0},
                new [] {0, 1, 0, 0},
                new [] {0, 0, 0, 0}
            }, _graph.ToMatrix().ToArray());
        }

        [Test]
        public void CorrectCreation()
        {
            try
            {
                var graph = new OrientedGraph(new List<Relation>
                {
                    new Relation(1, 2),
                    new Relation(1, 3),
                    new Relation(1, 4),
                    new Relation(3, 2),
                    new Relation(2, 1)
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.AreEqual(1, 1);
                return;
            }
            Assert.AreEqual(1, 2);
        }

        [Test]
        public void ConnectedGraphTest()
        {
            Assert.AreEqual(_notCoupledGraph.IsConnectedGraph(), false);
            Assert.AreEqual(_notCoupledGraph2.IsConnectedGraph(), false);
            Assert.AreEqual(_graph.IsConnectedGraph(), true);
        }
    }
}