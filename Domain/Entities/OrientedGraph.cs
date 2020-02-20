using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class OrientedGraph
    {
        private readonly IEnumerable<Relation> _relations;

        public OrientedGraph(IEnumerable<Relation> relations)
        {
            var relationsList = relations.ToList();
            foreach (var relation in relationsList)
            {
                if (relationsList.Any(it => it.Start == relation.End && it.End == relation.Start))
                {
                    throw new Exception("Ориентированный граф не может иметь ребер, направленных в обе стороны");
                }
            }
            
            _relations = relationsList;
        }

        public Matrix ToMatrix()
        {
            var relations = _relations.ToList();

            var max1 = _relations.Max(it => it.Start);
            var max2 = _relations.Max(it => it.End);
            var max = max1 > max2 ? max1 : max2;
            
            var matrix = new int[max][];
            for (var index = 0; index < max; index++)
            {
                matrix[index] = new int[max];
            }

            foreach (var relation in relations)
            {
                matrix[relation.Start - 1][relation.End - 1] = 1;
            }

            return new Matrix(matrix);
        }
    }
}