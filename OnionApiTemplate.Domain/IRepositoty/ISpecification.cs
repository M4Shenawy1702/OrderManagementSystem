using System.Linq.Expressions;

namespace OrderManagementSystem.Domain.IRepositoty
{
    public interface ISpecification<T> where T : class
    {
        Expression<Func<T, bool>>? Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>>? OrderBy { get; }
        Expression<Func<T, object>>? OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPaginated { get; }
    }
}
