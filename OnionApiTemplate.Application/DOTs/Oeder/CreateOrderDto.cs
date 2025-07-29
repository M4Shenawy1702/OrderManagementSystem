using OrderManagementSystem.Domain.Enums;

namespace OrderManagementSystem.Application.DOTs.Oeder
{
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
        public PaymentMethod PaymentMethod { get; set; }
    }
}
