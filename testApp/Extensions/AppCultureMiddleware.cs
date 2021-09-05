using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace testApp.Extensions
{
    public class AppCultureMiddleware
    {
        private readonly RequestDelegate _next;

        public AppCultureMiddleware(
            RequestDelegate next
        )
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Culture Middleware - Start");
            var cultureQuery = context.Request.Query["culture"];

            if(!string.IsNullOrWhiteSpace(cultureQuery))
            {
                var culture = new CultureInfo(cultureQuery);
                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;

                await context.Response.WriteAsync("This is a message from the culture middleware");
            }
            await _next(context);

            Console.WriteLine("Culture Middleware - End");
        }
    }
}