namespace Vega.Extensions
{
    public interface IQueryObject
    {
        string SortBy { get; set; }
        bool IsSortingAscending { get; set; }
        int Page { get; set; }
        byte PageSize { get; set; } 
    }
}