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
            
            var matrix = new int[relations.Count][];
            for (var index = 0; index < relations.Count; index++)
            {
                matrix[index] = new int[relations.Count];
            }

            foreach (var relation in relations)
            {
                matrix[relation.Start - 1][relation.End - 1] = 1;
            }

            return new Matrix(matrix);
        }
    }
}