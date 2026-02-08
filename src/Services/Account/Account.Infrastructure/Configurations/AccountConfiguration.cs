using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Infrastructure.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Domain.Aggregates.AccountAggregate.Account>
    {
        public void Configure(EntityTypeBuilder<Domain.Aggregates.AccountAggregate.Account> builder)
        {

            builder.HasQueryFilter(c => !c.IsDeleted);

            builder.HasIndex(a => a.AccountNumber)
                  .IsUnique();

            builder.Property(a => a.AccountNumber)
                  .HasMaxLength(20);

            builder.Property(t => t.Balance)
                .HasPrecision(18, 2);
        }
    }
}
