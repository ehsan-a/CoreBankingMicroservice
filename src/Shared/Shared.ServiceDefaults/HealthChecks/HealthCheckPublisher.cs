using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace Shared.ServiceDefaults.HealthChecks
{
    public class HealthCheckPublisher : IHealthCheckPublisher
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public HealthCheckPublisher(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();

            var logger =
                scope.ServiceProvider.GetRequiredService<ILogger<HealthCheckPublisher>>();

            if (report.Status == HealthStatus.Healthy)
            {
                logger.LogInformation("Liveness And Readiness Health Checks: Healthy");
            }
            else
            {
                logger.LogWarning("Liveness And Readiness Health Checks: UnHealthy");
            }

            return Task.CompletedTask;
        }
    }
}
