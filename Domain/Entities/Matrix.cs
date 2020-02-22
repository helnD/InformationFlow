using System;
using System.Linq;

namespace Domain.Entities
{
    public class Matrix : ICloneable
    {
        private readonly int[][] _matrix;

        public Matrix(int[][] matrix)
        {
            _matrix = matrix;
        }

        public int Height => _matrix.Length;

        public int Width => _matrix.First().Length;
        
        public Matrix Power(int power)
        {

            var result = _matrix.Clone() as int[][];
            
            for (var currentPower = 1; currentPower < power; currentPower++)
            {
                var newResult = new int[_matrix.Length][];
                for (var i = 0; i < _matrix.Length; i++)
                {
                    newResult[i] = new int[_matrix[i].Length];
                }

                var first = result;
                var second = _matrix;

                for (var firstRow = 0; firstRow < first.Length; firstRow++)
                {
                    for (var secondColumn = 0; secondColumn < second[0].Length; secondColumn++)
                    {
                        var sum = 0;
                        for (var secondRow = 0; secondRow < second.Length; secondRow++)
                        {
                            sum += first[firstRow][secondRow] * second[secondRow][secondColumn];
                        }

                        newResult[firstRow][secondColumn] = sum;
                    }
                }

                result = newResult;
            }

            return new Matrix(result);
        }

        public bool IsZeroMatrix()
        {
            foreach (var row in _matrix)
            {
                foreach (var cell in row)
                {
                    if (cell != 0) return false;
                }
            }

            return true;
        }

        public bool IsZeroColumn(int column) =>
            _matrix.All(row => row[column] == 0);

        public Matrix Plus(Matrix op)
        {
            var result = new int[_matrix.Length][];
            for (var row = 0; row < _matrix.Length; row++)
            {
                result[row] = new int[_matrix.Length];
                for (var column = 0; column < _matrix.Length; column++)
                {
                    result[row][column] = _matrix[row][column] + op[row][column];
                }
            }

            return new Matrix(result);
        }

        public override bool Equals(object obj)
        {
            var matrix2 = obj as Matrix;

            for (var row = 0; row < Height; row++)
            {
                for (var column = 0; column < Width; column++)
                {
                    if (this[row, column] != matrix2[row, column])
                        return false;
                }
            }

            return true;
        }

        public int[][] ToArray() => 
            _matrix.Clone() as int[][];

        public int this[int row, int column] =>
            _matrix[row][column];

        public int[] this[int row] =>
            _matrix[row];
        
        public object Clone()
        {
            return new Matrix(_matrix.Clone() as int[][]);
        }
    }
}