using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transaction.Domain.Aggregates.AccountTransactionAggregate;

namespace Transaction.Infrastructure.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<AccountTransaction>
    {
        public void Configure(EntityTypeBuilder<AccountTransaction> builder)
        {
            builder.Property(t => t.Amount)
            .HasPrecision(18, 2);
        }
    }
}
