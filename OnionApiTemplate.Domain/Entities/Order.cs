using OrderManagementSystem.Domain.Enums;

namespace OrderManagementSystem.Domain.Entities
{
    public class Order
        : BaseEntity<int>
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus Status { get; set; }

        public Customer Customer { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = [];
        public Invoice? Invoice { get; set; }
    }

}
