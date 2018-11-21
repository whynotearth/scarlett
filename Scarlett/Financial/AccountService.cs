namespace Scarlett.Financial
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Data.Entity;
    using Microsoft.EntityFrameworkCore;

    public class AccountService
    {
        protected ApplicationDbContext ApplicationDbContext { get; }
        
        protected ClaimsPrincipal User { get; }
        
        public AccountService(ApplicationDbContext applicationDbContext, ClaimsPrincipal user)
        {
            ApplicationDbContext = applicationDbContext;
            User = user;
        }
        
        public async Task AddBalance(Guid accountId, decimal amount)
        {
            var account = await ApplicationDbContext.Accounts.FindAsync(accountId);
            account.Balance += amount;
            await ApplicationDbContext.SaveChangesAsync();
        }

        public async Task DecreaseBalance(Guid accountId, decimal amount)
        {
            var account = await ApplicationDbContext.Accounts.FindAsync(accountId);
            account.Balance -= amount;
            await ApplicationDbContext.SaveChangesAsync();
        }
    }
}