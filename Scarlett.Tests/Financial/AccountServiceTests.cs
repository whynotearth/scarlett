namespace Scarlett.Tests.Financial
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Data.Entity;
    using Data.Entity.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Scarlett.Financial;
    using Xunit;

    public class AccountServiceTests : DatabaseTests
    {
        protected AccountService AccountService { get; }
        
        protected Guid AccountId { get; set; }
        
        public AccountServiceTests()
        {
            var principal = SignInManager.CreateUserPrincipalAsync(ApplicationDbContext.Users.First()).Result;
            AccountService = new AccountService(ServiceProvider.GetService<ApplicationDbContext>(), principal);
            var account = new Account
            {
                SavingsGroup = new SavingsGroup(),
                User = ApplicationDbContext.Users.First()
            };
            ApplicationDbContext.Accounts.Add(account);
            ApplicationDbContext.SaveChanges();
            AccountId = account.Id;
        }
        
        [Fact]
        public async Task AddBalance()
        {
            await AccountService.AddBalance(AccountId, 100);
            var account = await ApplicationDbContext.Accounts.AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == AccountId);
            Assert.Equal(100, account.Balance);
        }
        
        [Fact]
        public async Task DecreaseBalance()
        {
            var account = await ApplicationDbContext.Accounts
                .FirstOrDefaultAsync(a => a.Id == AccountId);
            account.Balance = 300;
            await ApplicationDbContext.SaveChangesAsync();
            await AccountService.DecreaseBalance(AccountId, 100);
            account = await ApplicationDbContext.Accounts.AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == AccountId);
            Assert.Equal(200, account.Balance);
        }
    }
}