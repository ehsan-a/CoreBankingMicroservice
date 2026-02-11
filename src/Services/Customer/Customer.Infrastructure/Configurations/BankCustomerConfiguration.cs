using Customer.Domain.Aggregates.BankCustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreBanking.Infrastructure.Persistence.Configurations
{
    public class BankCustomerConfiguration : IEntityTypeConfiguration<BankCustomer>
    {
        public void Configure(EntityTypeBuilder<BankCustomer> builder)
        {

            builder.HasQueryFilter(c => !c.IsDeleted);

        }
    }
}
