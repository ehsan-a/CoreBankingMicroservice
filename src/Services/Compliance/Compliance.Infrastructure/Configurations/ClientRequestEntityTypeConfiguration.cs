using Compliance.Infrastructure.Idempotency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compliance.Infrastructure.Configurations
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
