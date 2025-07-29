using OrderManagementSystem.Application.Specifications;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.Sepcifications
{
    internal class GetCustomerByIdSpecification
        : BaseSpecifications<Customer>
    {
        public GetCustomerByIdSpecification(int customerId)
            : base(c => c.Id == customerId)
        {
            AddInclude(c => c.User);
        }
    }
}
