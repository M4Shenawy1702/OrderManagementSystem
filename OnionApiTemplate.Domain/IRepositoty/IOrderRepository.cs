using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Domain.IRepositoty
{
    public interface IOrderRepository : IGenericRepository<Order, int>
    {
        Task<bool> UpdateStockAsync(int orderId);
    }
}
