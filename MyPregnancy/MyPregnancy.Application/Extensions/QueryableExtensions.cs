﻿namespace MyPregnancy.Application.Extensions
{
    using System.Linq;

    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(
            this IQueryable<T> source,
            IPaginationInfo pagination)
        {
            return source
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize);
        }
    }
}
