using Microsoft.AspNetCore.Identity;

namespace OrderManagementSystem.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Customer Customer { get; set; } = null!;
    }
}
