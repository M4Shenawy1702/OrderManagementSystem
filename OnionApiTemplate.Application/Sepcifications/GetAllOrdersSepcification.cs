using OrderManagementSystem.Application.Specifications;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.Sepcifications
{
    public class GetAllOrdersSepcification
        : BaseSpecifications<Order>
    {
        public GetAllOrdersSepcification()
            : base(null)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.OrderItems.Select(oi => oi.Product));
        }
    }
}
