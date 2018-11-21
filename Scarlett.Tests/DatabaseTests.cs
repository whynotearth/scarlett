namespace Scarlett.Tests
{
    using System;
    using System.Linq;
    using Data.Entity;
    using Data.Entity.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class DatabaseTests : IDisposable
    {
        protected ApplicationDbContext ApplicationDbContext { get; set; }

        protected IServiceProvider ServiceProvider => ServiceScope.ServiceProvider;
        protected IServiceScope ServiceScope { get; }

        protected SignInManager<User> SignInManager { get; }

        protected UserManager<User> UserManager { get; }

        protected DatabaseTests()
        {
            ServiceScope = DatabaseSingleton.Instance.GetNewServiceProvider();
            ApplicationDbContext = ServiceScope.ServiceProvider.GetService<ApplicationDbContext>();
            SignInManager = ServiceScope.ServiceProvider.GetService<SignInManager<User>>();
            UserManager = ServiceScope.ServiceProvider.GetService<UserManager<User>>();
            if (!ApplicationDbContext.Users.Any())
            {
                UserManager.CreateAsync(new User { UserName = "Test", Email = "test@test.com" }, "TestPassword1!")
                    .Wait();
            }

            ApplicationDbContext.Database.BeginTransaction();
        }

        public void Dispose()
        {
            ApplicationDbContext.Database.RollbackTransaction();
            ServiceScope.Dispose();
        }
    }
}