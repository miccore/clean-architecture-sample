using Microsoft.EntityFrameworkCore;

namespace Miccore.CleanArchitecture.Sample.Core.Utils;

public static class IncludeBuilder
{
    public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, 
        params string[] includes) where T : class
    {
        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }
        return query;
    }
}