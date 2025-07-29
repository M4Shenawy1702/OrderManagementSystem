using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.IRepositoty;
using OrderManagementSystem.Infrastructure.Persistence;
using OrderManagementSystem.Infrastructure.Persistence.Repositories;

public class UnitOfWork(ApplicationDbContext _context)
    : IUnitOfWork, IAsyncDisposable
{
    private readonly Dictionary<Type, object> _repositories = new();

    private IOrderRepository? _orderRepository;

    public IOrderRepository OrderRepository =>
        _orderRepository ??= new OrderRepository(_context);

    public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
        where TEntity : BaseEntity<TKey>
    {
        var entityType = typeof(TEntity);

        if (!_repositories.TryGetValue(entityType, out var repository))
        {
            repository = new GenericRepository<TEntity, TKey>(_context);
            _repositories[entityType] = repository;
        }

        return (IGenericRepository<TEntity, TKey>)repository!;
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public ValueTask DisposeAsync() => _context.DisposeAsync();
}
