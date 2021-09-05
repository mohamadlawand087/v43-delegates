using Microsoft.AspNetCore.Builder;

namespace testApp.Extensions
{
    public static class AppCultureMiddlewareExtentions
    {
        public static IApplicationBuilder UseAppCulture(
            this IApplicationBuilder builder
        )
        {
            // initialise middleware
            return builder.UseMiddleware<AppCultureMiddleware>();
        }
    }
}