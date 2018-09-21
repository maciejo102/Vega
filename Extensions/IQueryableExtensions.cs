using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vega.Contract.Models;

namespace Vega.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObject,
            Dictionary<string, Expression<Func<T, object>>> columnsMapping)
        {
            if (String.IsNullOrWhiteSpace(queryObject.SortBy) || !columnsMapping.ContainsKey(queryObject.SortBy))

                return query;
            if (queryObject.IsSortingAscending)
               return query = query.OrderBy(columnsMapping[queryObject.SortBy]);
            else 
               return query = query.OrderByDescending(columnsMapping[queryObject.SortBy]);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObject)
        {
            if (queryObject.Page <= 0)
                queryObject.Page = 1; 
            if (queryObject.PageSize <= 0)
                queryObject.PageSize = 10; 
            return query.Skip((queryObject.Page - 1) * queryObject.PageSize).Take(queryObject.PageSize);
        }
    }
}