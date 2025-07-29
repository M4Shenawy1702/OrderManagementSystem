using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.IServices;

namespace OrderManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{customerId}/orders")]
        public async Task<IActionResult> GetOrdersForCustomer(int customerId)
        {
            var orders = await _customerService.GetOrdersByCustomer(customerId);
            return Ok(orders);
        }
    }
}
