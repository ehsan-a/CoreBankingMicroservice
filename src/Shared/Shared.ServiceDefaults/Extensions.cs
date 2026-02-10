using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Shared.ServiceDefaults.HealthChecks;

namespace Shared.ServiceDefaults
{
    public static partial class Extensions
    {
        public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
        {
            builder.AddBasicServiceDefaults();

            return builder;
        }

        public static IHostApplicationBuilder AddBasicServiceDefaults(this IHostApplicationBuilder builder)
        {
            // Default health checks assume the event bus and self health checks
            builder.AddDefaultHealthChecks();

            return builder;
        }

        public static IHostApplicationBuilder AddDefaultHealthChecks(this IHostApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks()
                // Add a default liveness check to ensure app is responsive
                .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

            return builder;
        }

        public static WebApplication MapDefaultEndpoints(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.MapHealthChecks("/health",
                     new HealthCheckOptions
                     {
                         ResponseWriter = HealthCheckResponses.WriteResponse
                     });

                app.MapHealthChecks("/health/ready", new HealthCheckOptions
                {
                    Predicate = healthCheck => healthCheck.Tags.Contains("ready"),
                    ResponseWriter = HealthCheckResponses.WriteResponse
                });

                app.MapHealthChecks("/health/live", new HealthCheckOptions
                {
                    Predicate = _ => false,
                });
            }

            return app;
        }
    }
}
