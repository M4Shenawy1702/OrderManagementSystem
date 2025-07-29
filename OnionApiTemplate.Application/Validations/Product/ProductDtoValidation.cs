using FluentValidation;
using OrderManagementSystem.Application.DOTs.Product;

namespace OrderManagementSystem.Application.Validations.Product
{
    public class ProductDtoValidation : AbstractValidator<ProductDto>
    {
        public ProductDtoValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required")
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0");
            RuleFor(x => x.Stock)
                .NotEmpty().WithMessage("Stock is required")
                .GreaterThanOrEqualTo(0).WithMessage("Stock must be greater than or equal to 0");
        }
    }
}
