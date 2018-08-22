using Vega.Extensions;

namespace Vega.Contract.Models
{
    public class VehicleQuery : IQueryObject // Filter
    {
        public int? MakeId { get; set; }  
        public int? ModelId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortingAscending { get; set; }
    }
}