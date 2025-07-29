namespace OrderManagementSystem.Domain.IRepositoty
{
    public interface IDbInitializer
    {
        Task InitializeDatabaseAsync();
        Task InitializeIdentityAsync();
    }
}
