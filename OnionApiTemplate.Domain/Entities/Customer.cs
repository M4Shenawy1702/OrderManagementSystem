namespace OrderManagementSystem.Domain.Entities
{
    public class Customer
        : BaseEntity<int>
    {
        public string Name { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        public ICollection<Order> Orders { get; set; } = [];
    }
}
