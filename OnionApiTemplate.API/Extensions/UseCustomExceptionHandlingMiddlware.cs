using OrderManagementSystem.API.Middlewares;

namespace OrderManagementSystem.API.Extensions
{
    public static class CustomExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalErrorHandlingMiddleware>();
        }
    }
}
