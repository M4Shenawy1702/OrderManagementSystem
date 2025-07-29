using OrderManagementSystem.Domain.IRepositoty;
using System.Linq.Expressions;

namespace OrderManagementSystem.Application.Specifications
{
    public abstract class BaseSpecifications<T> : ISpecification<T> where T : class
    {
        protected BaseSpecifications(Expression<Func<T, bool>>? criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>>? Criteria { get; private set; }

        public List<Expression<Func<T, object>>> Includes { get; } = new();

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
            => Includes.Add(includeExpression);

        protected void SetOrderBy(Expression<Func<T, object>> orderByExpression)
            => OrderBy = orderByExpression;

        protected void SetOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
            => OrderByDescending = orderByDescExpression;

        protected void ApplyPagination(int pageSize, int pageIndex)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }
    }
}
