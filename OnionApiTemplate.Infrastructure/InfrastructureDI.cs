using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.IRepositoty;
using OrderManagementSystem.Infrastructure.Persistence;
using OrderManagementSystem.Infrastructure.Persistence.Repositories;

namespace OrderManagementSystem.Infrastructure
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentityCore<ApplicationUser>(config =>
            {
                //Email
                config.User.RequireUniqueEmail = true;
                //Password
                config.Password.RequireDigit = true;
                config.Password.RequireLowercase = true;
                config.Password.RequireUppercase = true;
                config.Password.RequireNonAlphanumeric = true;
                config.Password.RequiredLength = 8;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IOrderRepository, OrderRepository>();


            return services;
        }
    }
}
