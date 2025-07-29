using AutoMapper;
using OrderManagementSystem.Application.DOTs.Auth;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.MappingProfile
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterRequest, ApplicationUser>();
        }
    }
}
