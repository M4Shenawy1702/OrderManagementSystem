using OrderManagementSystem.Application.DOTs.Invoice;

namespace OrderManagementSystem.Application.IServices
{
    public interface IInvoiceService
    {
        Task<InvoiceResponseDto> GenerateInvoiceAsync(int orderId);
        Task<InvoiceResponseDto> GetInvoiceByIdAsync(int invoiceId);
        Task<List<InvoiceResponseDto>> GetAllInvoicesAsync();
    }
}

