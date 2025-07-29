using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Domain.IRepositoty
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IOrderRepository OrderRepository { get; }
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>;
    }
}
