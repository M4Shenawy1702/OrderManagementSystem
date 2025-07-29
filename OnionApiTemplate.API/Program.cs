using OrderManagementSystem.API.Extensions;
using OrderManagementSystem.Application;
using OrderManagementSystem.Domain.IRepositoty;
using OrderManagementSystem.Domain.Setting;
using OrderManagementSystem.Infrastructure;
using Stripe;

namespace OrderManagementSystem.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddWebApplicationServices(builder.Configuration);
            builder.Services.RegisterApplicationServices();

            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                await dbInitializer.InitializeDatabaseAsync();
                await dbInitializer.InitializeIdentityAsync();
            }

            app.UseCustomExceptionHandling();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAngularApp");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();


            await app.RunAsync();
        }
    }
}
