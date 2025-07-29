namespace OrderManagementSystem.Domain.Exceptions
{
    public class CustomerNotFoundException(int id)
    : NotFoundException($"Customer {id} not found");

}
