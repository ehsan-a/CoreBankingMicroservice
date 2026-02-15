using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Shared.ServiceDefaults.HealthChecks;

namespace Shared.ServiceDefaults
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddCustomHealthChecks(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var conStr = configuration.GetConnectionString("Connection")
                ?? throw new InvalidOperationException("Connection string not found.");

            services.AddHealthChecks()
            .AddSqlServer(conStr, tags: new[] { "ready" });

            services.Configure<HealthCheckPublisherOptions>(options =>
            {
                options.Delay = TimeSpan.FromSeconds(300);
                //options.Predicate = healthCheck => healthCheck.Tags.Contains("sample");
            });

            services.AddSingleton<IHealthCheckPublisher, HealthCheckPublisher>();


            return services;
        }
    }
}
