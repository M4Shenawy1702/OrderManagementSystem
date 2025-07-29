using OrderManagementSystem.Application.Specifications;
using OrderManagementSystem.Domain.Entities;

public class OrdersByCustomerSpecification
    : BaseSpecifications<Order>
{
    public OrdersByCustomerSpecification(int customerId)
        : base(o => o.CustomerId == customerId)
    {
    }
}
