namespace Scarlett.Data.Entity.Models
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class Transaction : IEntityTypeConfiguration<Transaction>
    {
        public Account Account { get; set; }

        public Guid AccountId { get; set; }

        public decimal Amount { get; set; }
        
        public Guid Id { get; set; }
        
        public User User { get; set; }
        
        public Guid UserId { get; set; }

        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("newsequentialid()");
            builder.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}