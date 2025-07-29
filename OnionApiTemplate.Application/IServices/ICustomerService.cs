using OrderManagementSystem.Application.DTOs;

namespace OrderManagementSystem.Application.IServices
{
    public interface ICustomerService
    {
        Task<List<OrderDto>> GetOrdersByCustomer(int customerId);
    }
}
