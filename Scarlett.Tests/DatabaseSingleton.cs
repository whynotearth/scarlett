namespace Scarlett.Tests
{
    using System;
    using System.IO;
    using Data.Entity;
    using Data.Entity.Models;
    using DependecyInjection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class DatabaseSingleton
    {
        public IConfiguration Configuration { get; }

        public static DatabaseSingleton Instance { get; } = new DatabaseSingleton();
        
        public Guid InstanceId { get; } = Guid.NewGuid();

        public IServiceProvider Services { get; }
        
        public IServiceCollection ServiceCollection { get; }

        private DatabaseSingleton()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.GetFullPath(@"..\..\..\appsettings.local.json"), true)
                .AddEnvironmentVariables()
                .Build();
            ServiceCollection = new ServiceCollection()
                .AddDbContext<ApplicationDbContext>(o =>
                    o.UseSqlServer(Configuration.GetConnectionString("Default").Replace("{ID}", InstanceId.ToString())))
                .AddScarlett();
            ServiceCollection
                .AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            Services = ServiceCollection.BuildServiceProvider();

            using (var scope = GetNewServiceProvider())
            {
                using (var dataContext = scope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    dataContext.Database.EnsureCreated();
                    AppDomain.CurrentDomain.ProcessExit += (s, e) => Dispose();
                    var seed = new DatabaseSeed();
                    seed.Seed(dataContext);
                }
            }
        }

        public IServiceScope GetNewServiceProvider()
        {
            return Services.CreateScope();
        }

        public void Dispose()
        {
            using (var scope = GetNewServiceProvider())
            {
                using (var dataContext = scope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    dataContext.Database.EnsureDeleted();
                }
            }
        }
    }
}