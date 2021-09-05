using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace testApp.Extensions
{
    public class AppSampleLogsMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("Middleware 1 started");
            await context.Response.WriteAsync("\nThis is the first middleware 1");
            await next(context);
            Console.WriteLine("Middleware 1 ended");
        }
    }
}