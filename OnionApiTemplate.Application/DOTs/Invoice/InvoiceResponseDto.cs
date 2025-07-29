namespace OrderManagementSystem.Application.DOTs.Invoice
{
    public class InvoiceResponseDto
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int OrderId { get; set; }
    }
}
