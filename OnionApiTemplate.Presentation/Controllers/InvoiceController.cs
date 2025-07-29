using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.IServices;

namespace OrderManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/invoices")]
    [Authorize(Roles = "Admin")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _invoiceService.GetAllInvoicesAsync();
            return Ok(invoices);
        }

        [HttpGet("{invoiceId}")]
        public async Task<IActionResult> GetInvoiceById(int invoiceId)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(invoiceId);
            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }
    }
}
