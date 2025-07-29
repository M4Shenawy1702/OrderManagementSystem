using FluentValidation;
using OrderManagementSystem.Application.DOTs.Auth;

namespace OrderManagementSystem.Application.Validations.Auth
{
    public class LoginRequestValidation : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
