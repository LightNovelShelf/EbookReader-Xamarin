using EbookReader.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.FileProviders;

namespace EbookReader.KestrelWebHost
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(configure =>
            {
                configure.EnableDetailedErrors = true;
            });
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(options => options.SetIsOriginAllowed(x => _ = true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithExposedHeaders("Content-Disposition"));

            var option = new FileServerOptions { EnableDirectoryBrowsing = true };
            option.StaticFileOptions.FileProvider = new PhysicalFileProvider(Server.Root);
            option.StaticFileOptions.ServeUnknownFileTypes = true;
            app.UseFileServer(option);

            app.UseSignalR(route =>
            {
                route.MapHub<ApiHub>("/api");
            });
        }
    }
}