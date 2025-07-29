using AutoMapper;
using OrderManagementSystem.Application.DOTs.Invoice;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.MappingProfile
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Invoice, InvoiceResponseDto>();
        }
    }
}
