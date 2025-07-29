using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.DOTs.Auth;
using OrderManagementSystem.Application.IServices;
namespace OrderManagementSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthService _authenticationService)
        : ControllerBase
    {

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromForm] LoginRequest request)
            => await _authenticationService.LoginAsync(request);

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register([FromForm] RegisterRequest request)
            => await _authenticationService.RegisterAsync(request);
    }
}
