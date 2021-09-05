using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using testApp.Extensions;

namespace testApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<AppSampleLogsMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAppCulture();

            // Run is a terminal delegate it does not pass the request to other 
            // delegates
            app.UseMiddleware<AppSampleLogsMiddleware>();

            //app.Run(async c => await c.Response.WriteAsync("\nWelcome to the terminal Middleware"));

            app.Use(async (context, next) => {
                Console.WriteLine("Middleware 2 started");
                await context.Response.WriteAsync("\nThis is the second middleware 2");
                await next();
                Console.WriteLine("Middleware 2 ended");
            });

             app.UseWhen(c => c.Request.Query.ContainsKey("s"), searchHandler);

            app.MapWhen(c => c.Request.Query.ContainsKey("life"), lifeHandler);

           
        }

        private void searchHandler(IApplicationBuilder app)
        {
            app.Use(async (context, next) => {
              Console.WriteLine("search middleware started");
              await context.Response.WriteAsync("\nI am your search handler");
                          

             await next();
               Console.WriteLine("search middleware ended");

          });
        }

        private void lifeHandler(IApplicationBuilder app)
        {
          app.Run(async context => {
              Console.WriteLine("Life middleware started");
              await context.Response.WriteAsync("\nI am your life handler");
                            Console.WriteLine("Life middleware ended");

          });
        }
    }
}
