using LayeredArch.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArch.Core.Application.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyFiltering<T>(this IQueryable<T> query, IQueryObject queryObj, Dictionary<string, Expression<Func<T, bool>>> columnsMap)
        {
            if (queryObj != null)
            {
                foreach (var prop in queryObj.GetType().GetProperties())
                {
                    if (prop.GetValue(queryObj) != null)
                    {
                        if (columnsMap.ContainsKey(prop.Name))
                        {
                            query = query.Where(columnsMap[prop.Name]);
                        }
                    }
                }
            }

            return query;
        }

        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObj, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if (String.IsNullOrWhiteSpace(queryObj.SortBy) || !columnsMap.ContainsKey(queryObj.SortBy))
            {
                return query;
            }

            if (queryObj.IsSortAscending.HasValue)
            {
                if (queryObj.IsSortAscending.Value == true)
                {
                    return query.OrderBy(columnsMap[queryObj.SortBy]);
                }
                else
                {
                    return query.OrderByDescending(columnsMap[queryObj.SortBy]);
                }
            }
            else
            {
                return query.OrderByDescending(columnsMap[queryObj.SortBy]);
            }
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObj)
        {
            if (queryObj.PageSize <= 0)
            {
                queryObj.PageSize = 10;
            }
            if (queryObj.Page <= 0)
            {
                queryObj.Page = 1;
            }

            return query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
        }

        public static IQueryable<T> ApplySearch<T>(this IQueryable<T> query, string searchKey)
          where T : ISearchable
        {
            var searchResult = new List<T>();
            foreach (var item in query)
            {
                if (item.SeacrhKey.IsLevenshtein(searchKey, 3))
                {
                    item.Similarity = (item.SeacrhKey.ToLower()).Levenshtein(searchKey.Trim().ToLower());
                    searchResult.Add(item);
                }
            }

            query = query
                    .Where(q => searchResult.Contains(q))
                    .AsQueryable();
            return query;
        }

    }
}
