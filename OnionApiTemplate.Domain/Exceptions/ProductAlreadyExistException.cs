namespace OrderManagementSystem.Domain.Exceptions
{
    public class ProductAlreadyExistException : BadRequestException
    {
        public ProductAlreadyExistException()
            : base(new List<string> { "Product already exists" })
        {
        }
    }
}
