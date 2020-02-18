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
        private IEnumerable<Tact> _tactsOfCreation;
        private IEnumerable<Tact> _tactsOfExtinction;
        private IEnumerable<Tact> _tactsOfStore;

        public GraphInfo(OrientedGraph graph)
        {
            _adjacencyMatrix = graph.ToMatrix();
        }

        public int Power { get; private set; }

        public IEnumerable<int> Outputs
        {
            get
            {
                if (_tactsOfCreation == null) _tactsOfCreation = TactsOfCreation();
                return _outputs;
            }
            private set => _outputs = value;
        }

        public IEnumerable<int> Inputs
        {
            get
            {
                if (_tactsOfCreation == null) _tactsOfCreation = TactsOfCreation();
                return _inputs;
            }
            private set => _inputs = value;
        }

        public IEnumerable<Tact> TactsOfCreation()
        {
            if (_tactsOfCreation != null) return _tactsOfCreation;

            var tacts = new List<Tact>();
            var currentTact = 0;

            Matrix matrix;

            do
            {
                matrix = _adjacencyMatrix.Power(currentTact + 1);

                for (int column = 0; column < matrix.Width; column++)
                {
                    if (matrix.IsZeroColumn(column) && tacts.All(it => it.Node != column + 1))
                    {
                        tacts.Add(new Tact(column + 1, currentTact));
                    }
                }

                currentTact++;

            } while (!matrix.IsZeroMatrix());

            Power = currentTact - 1;
            Outputs = tacts.FindAll(it => it.Value == currentTact - 1).Select(it => it.Node).OrderBy(it => it);
            Inputs = tacts.FindAll(it => it.Value == 0).Select(it => it.Node).OrderBy(it => it);

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

    }
}