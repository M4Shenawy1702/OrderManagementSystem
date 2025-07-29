using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.IRepositoty;

namespace OrderManagementSystem.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> UpdateStockAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) return false;

            foreach (var item in order.OrderItems)
            {
                if (item.Product == null || item.Product.Stock < item.Quantity)
                    return false;

                item.Product.Stock -= item.Quantity;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
