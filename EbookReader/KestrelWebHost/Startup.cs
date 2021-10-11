using EbookReader.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EbookReader.KestrelWebHost
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(options => options.SetIsOriginAllowed(x => _ = true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithExposedHeaders("Content-Disposition"));

            app.UseSignalR(route =>
            {
                route.MapHub<ApiHub>("/api");
            });

            app.Run(WebApp.OnHttpRequest);
        }
    }
}