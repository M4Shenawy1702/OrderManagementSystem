namespace OrderManagementSystem.Domain.Entities
{
    public class Invoice

        : BaseEntity<int>
    {
        public int OrderId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }

        public Order Order { get; set; } = null!;
    }

}
