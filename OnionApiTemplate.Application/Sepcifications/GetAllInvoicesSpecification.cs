using OrderManagementSystem.Application.Specifications;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.Sepcifications
{
    internal class GetAllInvoicesSpecification
        : BaseSpecifications<Invoice>
    {
        public GetAllInvoicesSpecification()
            : base(null)
        {
        }
    }
}
