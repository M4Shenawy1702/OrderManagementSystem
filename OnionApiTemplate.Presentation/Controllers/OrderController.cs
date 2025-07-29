using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.DOTs.Oeder;
using OrderManagementSystem.Application.IServices;
using OrderManagementSystem.Domain.Enums;

namespace OrderManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var result = await _orderService.CreateOrderAsync(dto);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrdersAsync();
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromQuery] OrderStatus newStatus)
        {
            await _orderService.UpdateOrderStatusAsync(id, newStatus);
            return NoContent();
        }
    }
}
