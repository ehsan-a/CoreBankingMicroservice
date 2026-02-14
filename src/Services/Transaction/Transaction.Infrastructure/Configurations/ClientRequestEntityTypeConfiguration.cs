using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transaction.Infrastructure.Idempotency;

namespace Transaction.Infrastructure.Configurations
{
    class ClientRequestEntityTypeConfiguration
    : IEntityTypeConfiguration<ClientRequest>
    {
        public void Configure(EntityTypeBuilder<ClientRequest> requestConfiguration)
        {
            requestConfiguration.ToTable("Requests");
        }
    }
}
