using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace EbookReader.KestrelWebHost
{
    class Server
    {
        public static IWebHost Host;
        public static int Port = 5555;

        public static Task CreateServer()
        {
            // 找到一个没有使用的端口
            while (InUse(Port))
            {
                Port++;
            }

            var webHost = new WebHostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IHostLifetime, ConsoleLifetimePatch>();
                })
                .UseKestrel(options =>
                {
                    options.ListenAnyIP(Port);
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            Host = webHost;

            return webHost.RunPatchedAsync();
        }

        static bool InUse(int port)
        {
            var process = new Process
            {
                StartInfo = { UseShellExecute = false, FileName = "lsof", RedirectStandardOutput = true, Arguments = $" -i:{port}" }
            };
            process.Start();
            process.WaitForExit();
            var result = process.StandardOutput.ReadToEnd();
            process.Dispose();
            return !string.IsNullOrWhiteSpace(result);
        }
    }
}