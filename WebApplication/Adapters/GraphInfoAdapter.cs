using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.UseCase;
using WebApplication.ViewEntities;
using BMatrix = WebApplication.ViewEntities.BMatrix;
using GraphInfo = WebApplication.ViewEntities.GraphInfo;

namespace WebApplication.Adapters
{
    public class GraphInfoAdapter
    {
        public TactInfo[] AdaptTacts(IEnumerable<Tact> creation, IEnumerable<Tact> extinction, IEnumerable<Tact> store)
        {
            var count = creation.Count();

            if (extinction.Count() != count || store.Count() != count)
            {
                throw new Exception("Невозможно адаптировать такты из-за их разного количества");
            }

            var result = new List<TactInfo>();

            var creationList = creation.OrderBy(it => it.Node).ToList();
            var extinctionList = extinction.OrderBy(it => it.Node).ToList();
            var storeList = store.OrderBy(it => it.Node).ToList();

            for (var index = 0; index < count; index++)
            {
                var node = index + 1;
                var creationTact = creationList[index].Value;
                var extinctionTact = extinctionList[index].Value;
                var storeTact = storeList[index].Value;
                
                var newInfo = new TactInfo(node, creationTact, extinctionTact, storeTact);
                
                result.Add(newInfo);
            }

            return result.ToArray();
        }

        public GraphInfo AdaptGraphInfo(int power, IEnumerable<int> inputs, IEnumerable<int> outputs) =>
            new GraphInfo(power, inputs.ToArray(), outputs.ToArray());
        
    }
}