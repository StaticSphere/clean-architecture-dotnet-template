using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CleanArchTemplate.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        // TODO: Remove this line if you want to return the Server header
                        .ConfigureKestrel(config => config.AddServerHeader = false)
                        .UseStartup<Startup>();
                });
    }
}
