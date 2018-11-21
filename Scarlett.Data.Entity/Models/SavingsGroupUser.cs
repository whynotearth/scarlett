namespace Scarlett.Data.Entity.Models
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SavingsGroupUser : IEntityTypeConfiguration<SavingsGroupUser>
    {
        public Guid Id { get; set; }

        public SavingsGroup SavingsGroup { get; set; }

        public Guid SavingsGroupId { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }

        public void Configure(EntityTypeBuilder<SavingsGroupUser> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("newsequentialid()");
        }
    }
}