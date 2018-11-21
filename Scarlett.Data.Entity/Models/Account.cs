namespace Scarlett.Data.Entity.Models
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class Account : IEntityTypeConfiguration<Account>
    {
        public decimal Balance { get; set; }

        public Guid CreationDate { get; set; }

        public Guid Id { get; set; }

        public decimal InterestRate { get; set; }

        public decimal IntervalPaymentAmount { get; set; }

        public decimal NextPaymentDueAmount { get; set; }

        public Guid NextPaymentDueDate { get; set; }

        public SavingsGroup SavingsGroup { get; set; }

        public Guid SavingsGroupId { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }

        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("newsequentialid()");
        }
    }
}