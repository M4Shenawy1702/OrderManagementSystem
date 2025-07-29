using OrderManagementSystem.Application.Specifications;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.Sepcifications
{
    public class GetOrderByIdSepcification
        : BaseSpecifications<Order>
    {
        public GetOrderByIdSepcification(int orderId)
            : base(o => o.Id == orderId)
        {
            AddInclude(o => o.OrderItems);
        }
    }
}
