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
        public static int Port = 5556;
        public static string Root;

        public static Task CreateServer()
        {
            try
            {
                // 找到一个没有使用的端口
                while (InUse(Port))
                {
                    Port++;
                }

                Root = new DirectoryInfo(Path.Join(MainActivity.Activity.ExternalCacheDir.AbsolutePath, "..")).FullName;

                var webHost = new WebHostBuilder()
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddSingleton<IHostLifetime, ConsoleLifetimePatch>();
                    })
                    .UseKestrel(options =>
                    {
                        options.ListenAnyIP(Port);
                    })
                    .UseContentRoot(Root)
                    .UseStartup<Startup>()
                    .Build();

                Host = webHost;

                return webHost.RunPatchedAsync();
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.ToString());
                throw;
            }
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