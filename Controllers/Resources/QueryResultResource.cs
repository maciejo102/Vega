using System.Collections.Generic;

namespace Vega.Controllers.Resources
{
    public class QueryResultResource<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}