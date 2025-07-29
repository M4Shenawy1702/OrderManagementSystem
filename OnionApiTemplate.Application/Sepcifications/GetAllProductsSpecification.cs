using OrderManagementSystem.Application.DOTs.Product;
using OrderManagementSystem.Application.Specifications;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.Sepcifications
{
    public class GetAllProductsSpecification : BaseSpecifications<Product>
    {
        public GetAllProductsSpecification(ProductQueryParameters productQuery)
            : base(p =>
                (string.IsNullOrWhiteSpace(productQuery.Search) || p.Name.ToLower().Contains(productQuery.Search.ToLower()))
            )
        {
            if (productQuery.SortOption.HasValue)
            {
                switch (productQuery.SortOption.Value)
                {
                    case ProductSortOption.PriceAsc:
                        SetOrderBy(p => p.Price);
                        break;
                    case ProductSortOption.PriceDesc:
                        SetOrderByDescending(p => p.Price);
                        break;
                    case ProductSortOption.NameAsc:
                        SetOrderBy(p => p.Name);
                        break;
                    case ProductSortOption.NameDesc:
                        SetOrderByDescending(p => p.Name);
                        break;
                    default:
                        SetOrderBy(p => p.Name);
                        break;
                }
            }

            ApplyPagination(productQuery.PageSize, productQuery.PageIndex);
        }
    }
}
