using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;

namespace Laboratorio8.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(kesterl =>
                    {
                        kesterl.AddServerHeader = false;    // OWASP: Remove Kestrel response header
                    });
                    webBuilder.UseStartup<Startup>();
                })
                .UseNLog();
        }
    }
}
