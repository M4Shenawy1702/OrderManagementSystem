namespace OrderManagementSystem.Domain.Exceptions
{
    public class NotFoundException(string id)
        : Exception($"{id} not found");
}
