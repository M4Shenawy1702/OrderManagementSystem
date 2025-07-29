using OrderManagementSystem.Application.DOTs.Auth;

namespace OrderManagementSystem.Application.IServices
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
    }
}
