using OrderManagementSystem.Application.Specifications;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.Sepcifications
{
    internal class GetProductByNameSpecification
        : BaseSpecifications<Product>
    {
        public GetProductByNameSpecification(string name)
            : base(p => p.Name == name)
        {
        }
    }
}
