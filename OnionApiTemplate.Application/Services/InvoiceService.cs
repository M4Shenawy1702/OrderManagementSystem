using AutoMapper;
using OrderManagementSystem.Application.DOTs.Invoice;
using OrderManagementSystem.Application.IServices;
using OrderManagementSystem.Application.Sepcifications;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Exceptions;
using OrderManagementSystem.Domain.IRepositoty;

namespace OrderManagementSystem.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<InvoiceResponseDto> GenerateInvoiceAsync(int orderId)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, int>();
            var invoiceRepo = _unitOfWork.GetRepository<Invoice, int>();

            var order = await orderRepo.GetByIdAsync(orderId)
                        ?? throw new OrderNotFoundException(orderId);

            if (order.Status != Domain.Enums.OrderStatus.Paid)
                throw new Exception("Invoice can't be generated for an unpaid order.");

            var invoice = new Invoice
            {
                OrderId = orderId,
                InvoiceDate = DateTime.UtcNow,
                TotalAmount = order.TotalAmount
            };

            await invoiceRepo.AddAsync(invoice);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<InvoiceResponseDto>(invoice);
        }

        public async Task<InvoiceResponseDto> GetInvoiceByIdAsync(int invoiceId)
        {
            var repo = _unitOfWork.GetRepository<Invoice, int>();
            var invoice = await repo.GetByIdAsync(invoiceId)
                          ?? throw new Exception("Invoice not found");

            return new InvoiceResponseDto
            {
                InvoiceId = invoice.Id,
                InvoiceDate = invoice.InvoiceDate,
                TotalAmount = invoice.TotalAmount,
                OrderId = invoice.OrderId
            };
        }

        public async Task<List<InvoiceResponseDto>> GetAllInvoicesAsync()
        {
            var repo = _unitOfWork.GetRepository<Invoice, int>();
            var invoices = await repo.GetAllAsync(new GetAllInvoicesSpecification());

            return invoices.Select(i => new InvoiceResponseDto
            {
                InvoiceId = i.Id,
                InvoiceDate = i.InvoiceDate,
                TotalAmount = i.TotalAmount,
                OrderId = i.OrderId
            }).ToList();
        }
    }
}
