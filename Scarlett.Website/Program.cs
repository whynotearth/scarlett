namespace Scarlett.Website
{
    using System.IO;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    public class Program
    {
        public static void Main(string[] args)
        {
            PrivateCreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder PrivateCreateWebHostBuilder(string[] args)
        {
            return CreateWebHostBuilder(args)
                .UseSetting("DesignTime", "true");
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddCommandLine(args)
                    .AddEnvironmentVariables()
                    .AddJsonFile("appsettings.local.json", true, true)
                    .Build()
                )
                .UseStartup<Startup>();
        }
    }
}