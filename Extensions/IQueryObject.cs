namespace Vega.Extensions
{
    public interface IQueryObject
    {
         string SortBy { get; set; }
         bool IsSortingAscending { get; set; }
    }
}