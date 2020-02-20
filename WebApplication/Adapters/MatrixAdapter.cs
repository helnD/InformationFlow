using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using WebApplication.ViewEntities;

namespace WebApplication.Adapters
{
    public class MatrixAdapter
    {
        public int[][][] AdaptAdjacencyMatrixWithPowers(Matrix adjacencyMatrix, IEnumerable<Matrix> powers)
        {
            var result = new List<int[][]>
            {
                adjacencyMatrix.ToArray()
            };
            result.AddRange(powers.Select(power => power.ToArray()));

            return result.ToArray();
        }

        public BMatrix AdaptBMatrix(Domain.UseCase.BMatrix matrix)
        {
            
            var sum = new List<int>();
            
            for (var index = 0; index < matrix.Height; index++)
            {
                sum.Add(matrix.SumOfRow(index));
            }
            
            return new BMatrix(matrix.ToArray(), sum.ToArray());
        }
    }
}