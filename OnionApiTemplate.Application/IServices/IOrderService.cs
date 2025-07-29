using OrderManagementSystem.Application.DOTs.Oeder;
using OrderManagementSystem.Domain.Enums;

namespace OrderManagementSystem.Application.IServices
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto dto);
        Task<OrderResponseDto> GetOrderByIdAsync(int orderId);
        Task<List<OrderResponseDto>> GetAllOrdersAsync();
        Task UpdateOrderStatusAsync(int orderId, OrderStatus newStatus);
    }

}
