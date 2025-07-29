using AutoMapper;
using OrderManagementSystem.Application.DOTs.Oeder;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.MappingProfile
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderItem, OrderItemDetailDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        }
    }
}
