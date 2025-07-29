namespace OrderManagementSystem.Domain.Exceptions
{
    public class OrderNotFoundException(int id)
        : NotFoundException($"Order {id} not found");
}
