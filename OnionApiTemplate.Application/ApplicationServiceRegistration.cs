using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Application.IServices;
using OrderManagementSystem.Application.MappingProfile;
using OrderManagementSystem.Application.Services;
using OrderManagementSystem.Application.Validations.Auth;
using OrderManagementSystem.Application.Validations.Product;

namespace OrderManagementSystem.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            // Register all validators in the assembly
            services.AddValidatorsFromAssembly(typeof(LoginRequestValidation).Assembly);
            services.AddAutoMapper(typeof(UserProfile).Assembly);
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddValidatorsFromAssemblyContaining<ProductDtoValidation>();



            return services;
        }
    }
}
