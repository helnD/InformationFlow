using System.Collections.Generic;
using Domain.Entities;
using Domain.UseCase;
using NUnit.Framework;

namespace Tests
{
    public class BMatrixTests
    {
        
        private OrientedGraph _graph = new OrientedGraph(new List<Relation>
        {
            new Relation(1, 2),
            new Relation(2, 3),
            new Relation(2, 7),
            new Relation(4, 1),
            new Relation(4, 2),
            new Relation(5, 2),
            new Relation(6, 4),
            new Relation(8, 5)
        });
        
        [Test]
        public void BMatrixTest()
        {
            var bMatrix = new BMatrix(_graph.ToMatrix()).ToArray();

            var res = new[]
            {
                new[] {0, 1, 1, 0, 0, 0, 1, 0},
                new[] {0, 0, 1, 0, 0, 0, 1, 0},
                new[] {0, 0, 0, 0, 0, 0, 0, 0},
                new[] {1, 2, 2, 0, 0, 0, 2, 0},
                new[] {0, 1, 1, 0, 0, 0, 1, 0},
                new[] {1, 2, 2, 1, 0, 0, 2, 0},
                new[] {0, 0, 0, 0, 0, 0, 0, 0},
                new[] {0, 1, 1, 0, 1, 0, 1, 0}
            };
            
            Assert.AreEqual(res, bMatrix);
        }
    }
}