using Account.Domain.Aggregates.BankAccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Infrastructure.Configurations
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
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
