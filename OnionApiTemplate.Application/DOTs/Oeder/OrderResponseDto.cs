namespace OrderManagementSystem.Application.DOTs.Oeder
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;
        public string PaymentUrl { get; set; } = null!;
        public List<OrderItemDetailDto> Items { get; set; } = new();
    }

    public class OrderItemDetailDto
    {
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
