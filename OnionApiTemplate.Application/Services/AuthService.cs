namespace OrderManagementSystem.Application.Services
{
    public class AuthService(UserManager<ApplicationUser> userManager,
        IOptions<JWT> jwtOptions, IValidator<LoginRequest> loginValidator,
        IMapper mapper, IValidator<RegisterRequest> registerValidator,
        IUnitOfWork unitOfWork)
        : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly JWT _jwtOptions = jwtOptions.Value;
        private readonly IValidator<LoginRequest> _loginValidator = loginValidator;
        private readonly IValidator<RegisterRequest> _registerValidator = registerValidator;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var validationResult = await _loginValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new BadRequestException(errors);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                throw new InvalidCredentialsException();

            return new AuthResponse(user.Email!, user.UserName!, await CreateJWTTokenAsync(user));
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var validationResult = await _registerValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new BadRequestException(errors);
            }

            if (await _userManager.FindByEmailAsync(request.Email) is not null)
                throw new UserAlreadyExistsException("Email already exists");

            if (await _userManager.FindByNameAsync(request.UserName) is not null)
                throw new UserAlreadyExistsException("Username already exists");

            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.UserName,

            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new BadRequestException(result.Errors.Select(e => e.Description).ToList());

            var customer = new Customer
            {
                Name = request.Name,
                UserId = user.Id,
            };

            var customerRepo = _unitOfWork.GetRepository<Customer, int>();
            await customerRepo.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            await _userManager.AddToRoleAsync(user, "Customer");

            var token = await CreateJWTTokenAsync(user);
            return new AuthResponse(user.Email!, user.UserName!, token);
        }

        private async Task<string> CreateJWTTokenAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.DurationInMinutes),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
