using Domain.Entities;
using NUnit.Framework;

namespace Tests
{
    public class MatrixTests
    {

        private Matrix _matrix = new Matrix(new[]
        {
            new[] {1, 2, 3},
            new[] {1, 2, 3},
            new[] {3, 2, 1}
        });
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Power()
        {
            Assert.AreEqual(new[]
            {
                new[] {12, 12, 12},
                new[] {12, 12, 12},
                new[] {8, 12, 16}
            }, _matrix.Power(2).ToArray());
            
            Assert.AreEqual(new[]
            {
                new[] {60, 72, 84},
                new[] {60, 72, 84},
                new[] {68, 72, 76}
            }, _matrix.Power(3).ToArray());
        }
    }
}