using OrderManagementSystem.Application.DOTs;
using OrderManagementSystem.Application.DOTs.Product;

namespace OrderManagementSystem.Application.IServices
{
    public interface IProductService
    {
        Task<int> CreateAsync(ProductDto dto);
        Task<ProductDetailsDto> UpdateAsync(int id, ProductDto dto);
        Task DeleteAsync(int id);
        Task<ProductDetailsDto> GetByIdAsync(int id);
        Task<PaginatedResult<ProductDetailsDto>> GetAllAsync(ProductQueryParameters productQuery);
    }
}
