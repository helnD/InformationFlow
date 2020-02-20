using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Domain.UseCase
{
    public class BMatrix
    {
        private readonly Matrix _matrix;

        public BMatrix(Matrix matrix)
        {
            _matrix = Calculate(matrix);
        }

        public int Height => _matrix.Height;

        private Matrix Calculate(Matrix matrix)
        {

            var result = matrix;
            var tempMatrix = matrix;
            var power = 2;

            while (!tempMatrix.IsZeroMatrix())
            {
                tempMatrix = matrix.Power(power);
                result = result.Plus(tempMatrix);
                power++;
            }

            return result;
        }

        public int SumOfRow(int row) =>
            _matrix[row].Sum();

        public int this[int row, int column] =>
            _matrix[row][column];

        public int[][] ToArray() =>
            _matrix.ToArray();
    }
}