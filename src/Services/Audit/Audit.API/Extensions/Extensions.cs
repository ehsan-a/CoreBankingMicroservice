using Audit.Application.IntegrationEvents.EventHandling;
using Audit.Application.IntegrationEvents.Events;
using Audit.Application.Interfaces;
using Audit.Application.Mappings;
using Audit.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared.EventBus.Abstractions;
using Shared.EventBus.Extensions;
using Shared.EventBusRabbitMQ;
using Shared.Infrastructure.Middlewares;
using Shared.ServiceDefaults;

namespace Audit.API.Extensions
{
    internal static class Extensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            var services = builder.Services;

            builder.AddDefaultAuthentication();

            services.AddDbContext<AuditDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")
                ?? throw new InvalidOperationException("Connection string 'AuditDbContext' not found.")));

            //services.AddMigration<AccountDbContext, AccountDbContextSeed>();

            builder.AddRabbitMqEventBus("eventbus")
                   .AddEventBusSubscriptions();

            services.AddHttpContextAccessor();

            services.AddScoped<IAuditLogService, AuditLogService>();

            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(AuditLogProfile).Assembly));
            services.AddCustomCors(builder.Configuration);
            services.AddAuthorizationPolicies();
            services.AddSwaggerDocumentation();
            services.AddRateLimiting(builder.Configuration);
            services.AddCustomHealthChecks(builder.Configuration);
        }

        private static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
        {
            eventBus.AddSubscription<BankAccountCreatedIntegrationEvent, BankAccountCreatedIntegrationEventHandler>();
            eventBus.AddSubscription<BankCustomerCreatedIntegrationEvent, BankCustomerCreatedIntegrationEventHandler>();
        }

        public static IApplicationBuilder UseCustomMiddlewares(
          this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseMiddleware<GlobalExceptionMiddleware>();

            return app;
        }
    }
}
