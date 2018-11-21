namespace Scarlett.Data.Entity
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid> 
    {
        public DbSet<Account> Accounts { get; set; }
        
        public DbSet<SavingsGroup> SavingsGroups { get; set; }
        
        public DbSet<Transaction> Transactions { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().Property(e => e.Id).HasDefaultValueSql("newsequentialid()");
            builder.Entity<Role>().Property(e => e.Id).HasDefaultValueSql("newsequentialid()");
            var implementedConfigTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => !t.IsAbstract
                            && !t.IsGenericTypeDefinition
                            && t.GetTypeInfo().ImplementedInterfaces.Any(i =>
                                i.GetTypeInfo().IsGenericType &&
                                i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var configType in implementedConfigTypes)
            {
                dynamic config = Activator.CreateInstance(configType);
                builder.ApplyConfiguration(config);
            }
        }
    }
}