using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.UseCase;
using NUnit.Framework;

namespace Tests
{
    public class GraphInfoTests
    {

        private GraphInfo _info = new GraphInfo(new OrientedGraph(new List<Relation>
        {
            new Relation(1, 2),
            new Relation(2, 3),
            new Relation(2, 7),
            new Relation(4, 1),
            new Relation(4, 2),
            new Relation(5, 2),
            new Relation(6, 4),
            new Relation(8, 5)
        }));

        [Test]
        public void TactOfCreationTest()
        {
            Assert.AreEqual(new []
            {
                "12", "23", "34", "41", "51", "60", "74", "80"
            }, _info.TactsOfCreation().Select(it => $"{it.Node}{it.Value}"));
        }

        [Test]
        public void TactOfExtinction()
        {
            Assert.AreEqual(new []
            {
                "13", "24", "34", "43", "53", "61", "74", "81"
            }, _info.TactsOfExtinction().Select(it => $"{it.Node}{it.Value}"));
        }
        
        [Test]
        public void TactOfStore()
        {
            Assert.AreEqual(new []
            {
                "11", "21", "30", "42", "52", "61", "70", "81"
            }, _info.TactsOfStore().Select(it => $"{it.Node}{it.Value}"));
        }

        [Test]
        public void CommonInfo()
        {
            Assert.AreEqual(new List<int> { 3, 7 }, _info.Outputs);
            Assert.AreEqual(new List<int> { 6, 8 }, _info.Inputs);
            Assert.AreEqual(4, _info.Power);
        }
    }
}