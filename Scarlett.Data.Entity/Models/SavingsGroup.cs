namespace Scarlett.Data.Entity.Models
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SavingsGroup : IEntityTypeConfiguration<SavingsGroup>
    {
        public Guid Id { get; set; }
        
        public string Label { get; set; }
        
        public void Configure(EntityTypeBuilder<SavingsGroup> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(e => e.Label).HasMaxLength(64);
        }
    }
}