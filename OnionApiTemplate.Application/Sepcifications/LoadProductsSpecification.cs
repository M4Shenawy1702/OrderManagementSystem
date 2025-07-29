using OrderManagementSystem.Application.Specifications;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.Sepcifications
{
    internal class LoadProductsSpecification
        : BaseSpecifications<Product>
    {
        public LoadProductsSpecification(List<int> productIds)
            : base(p => productIds.Contains(p.Id))
        {
        }
    }
}
