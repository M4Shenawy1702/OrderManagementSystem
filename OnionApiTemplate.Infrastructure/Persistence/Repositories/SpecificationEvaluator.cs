using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.IRepositoty;

namespace OrderManagementSystem.Infrastructure.Persistence.Repositories
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, ISpecification<T> spec) where T : class
        {
            var query = inputQuery;

            // Apply filter
            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            // Apply includes
            foreach (var include in spec.Includes)
                query = query.Include(include);

            // Apply ordering
            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDescending is not null)
                query = query.OrderByDescending(spec.OrderByDescending);

            // Apply pagination
            if (spec.IsPaginated)
                query = query.Skip(spec.Skip).Take(spec.Take);

            return query;
        }
    }
}
