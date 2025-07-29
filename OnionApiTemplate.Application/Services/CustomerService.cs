namespace OrderManagementSystem.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<OrderDto>> GetOrdersByCustomer(int customerId)
        {
            var repo = _unitOfWork.GetRepository<Order, int>();
            var orders = await repo.GetAllAsync(new OrdersByCustomerSpecification(customerId));

            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                TotalAmount = o.TotalAmount,
                Status = o.Status.ToString(),
                CreatedAt = o.OrderDate
            }).ToList();
        }
    }
}
