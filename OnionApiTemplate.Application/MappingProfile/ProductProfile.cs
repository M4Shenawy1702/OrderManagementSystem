using AutoMapper;
using OrderManagementSystem.Application.DOTs.Product;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.MappingProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDetailsDto>();
        }
    }
}
