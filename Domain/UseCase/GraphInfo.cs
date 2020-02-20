using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Domain.UseCase
{
    public class GraphInfo
    {
        private readonly Matrix _adjacencyMatrix;
        private IEnumerable<int> _inputs;
        private IEnumerable<int> _outputs;
        private int _power;
        private IEnumerable<Tact> _tactsOfCreation;
        private IEnumerable<Tact> _tactsOfExtinction;
        private IEnumerable<Tact> _tactsOfStore;

        private IEnumerable<Matrix> _matricesToZero;

        public GraphInfo(OrientedGraph graph)
        {
            _adjacencyMatrix = graph.ToMatrix();
        }

        public IEnumerable<Matrix> MatricesToZero
        {
            get
            {
                _matricesToZero ??= GetMatricesToZero();
                return _matricesToZero;
            }
        }

        public int Power
        {
            get
            {
                _matricesToZero ??= GetMatricesToZero();
                return _matricesToZero.Count() - 1;
            }
        }

        public IEnumerable<int> Outputs
        {
            get
            {
                _outputs ??= GetOutputs();
                return _outputs;
            }
        }
            

        public IEnumerable<int> Inputs 
        {
            get
            {
                _inputs ??= GetInputs();
                return _inputs;
            }
        }

        public IEnumerable<Tact> TactsOfCreation()
        {
            if (_tactsOfCreation != null) return _tactsOfCreation;

            _matricesToZero ??= GetMatricesToZero();

            var tacts = new List<Tact>();
            var currentTact = 0;

            foreach (var matrix1 in _matricesToZero)
            {
                for (var column = 0; column < matrix1.Width; column++)
                {
                    if (matrix1.IsZeroColumn(column) && tacts.All(it => it.Node != column + 1))
                    {
                        tacts.Add(new Tact(column + 1, currentTact));
                    }
                }
                currentTact++;
            }

            tacts.Sort((first, second) => first.Node.CompareTo(second.Node));

            _tactsOfCreation = tacts;

            return tacts;
        }

        public IEnumerable<Tact> TactsOfExtinction()
        {

            if (_tactsOfExtinction != null) return _tactsOfCreation;

            if (_tactsOfCreation == null)
            {
                _tactsOfCreation = TactsOfCreation();
            }
            
            var tactsOfCreation = _tactsOfCreation.ToList();
            
            var result = new List<Tact>();

            for (var row = 0; row < _adjacencyMatrix.Height; row++)
            {

                if (Outputs.Contains(row + 1))
                {
                    result.Add(new Tact(row + 1, Power));
                    continue;
                }
                
                var maxCell = 0;
                for (var node = 0; node < _adjacencyMatrix.Height; node++)
                {

                    var currentCell = _adjacencyMatrix[row, node] * tactsOfCreation[node].Value;
                    if (maxCell < currentCell)
                    {
                        maxCell = currentCell;
                    }
                }
                result.Add(new Tact(row + 1, maxCell));
            }

            _tactsOfExtinction = result.OrderBy(it => it.Node);

            return _tactsOfExtinction;
        }
        
        public IEnumerable<Tact> TactsOfStore()
        {

            if (_tactsOfExtinction == null)
            {
                _tactsOfExtinction = TactsOfExtinction();
            }

            if (_tactsOfStore != null) return _tactsOfStore;
            
            var extinction = _tactsOfExtinction.ToList();
            var creation = _tactsOfCreation.ToList();
            var result = new List<Tact>();

            for (var index = 0; index < creation.Count; index++)
            {
                var tactNumber = extinction[index].Value - creation[index].Value;
                result.Add(new Tact(index + 1, tactNumber));
            }

            _tactsOfStore = result;
            
            return result;
        }


        private IEnumerable<Matrix> GetMatricesToZero()
        {
            var result = new List<Matrix>();
            
            var currentTact = 0;
            Matrix matrix;

            do
            {
                matrix = _adjacencyMatrix.Power(currentTact + 1);

                result.Add(matrix);

                currentTact++;

            } while (!matrix.IsZeroMatrix());

            return result;
        }
        
        private IEnumerable<int> GetInputs()
        {
            var result = new List<int>();

            for (var column = 0; column < _adjacencyMatrix.Width; column++)
            {
                var columnSum = 0;
                
                for (var row = 0; row < _adjacencyMatrix.Height; row++)
                {
                    columnSum += _adjacencyMatrix[row, column];
                }

                if (columnSum == 0)
                {
                    result.Add(column + 1);
                }
            }

            return result;
        }
        
        private IEnumerable<int> GetOutputs()
        {
            var result = new List<int>();

            for (var row = 0; row < _adjacencyMatrix.Height; row++)
            {
                var rowSum = 0;
                
                for (var column = 0; column < _adjacencyMatrix.Width; column++)
                {
                    rowSum += _adjacencyMatrix[row, column];
                }

                if (rowSum == 0)
                {
                    result.Add(row + 1);
                }
            }

            return result;
        }

    }
}