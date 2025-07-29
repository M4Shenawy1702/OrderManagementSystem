namespace OrderManagementSystem.Domain.Exceptions
{
    public class UserAlreadyExistsException(string message)
        : BadRequestException(new List<string> { message });
}
