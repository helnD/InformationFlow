using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Adapters
{
    public class RelationsAdapter
    {
        private IQueryCollection _query;

        public RelationsAdapter(IQueryCollection query)
        {
            _query = query;
        }

        public IEnumerable<Relation> Adapt()
        {
            var keys = new List<string>();

            foreach (var key in _query.Keys)
            {
                var number = key.Substring(1);
                if (keys.Any(it => number == it)) continue;
                
                keys.Add(number);
            }
            
            var result = new List<Relation>();

            foreach (var key in keys)
            {
                var start = int.Parse("s" + key);
                var end = int.Parse("s" + key);
                
                result.Add(new Relation(start, end));
            }

            return result;
        }
    }
}