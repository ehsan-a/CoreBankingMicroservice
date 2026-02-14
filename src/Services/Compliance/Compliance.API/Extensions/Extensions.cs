using Compliance.API.HealthChecks;
using Compliance.Application;
using Compliance.Application.Commands;
using Compliance.Application.IntegrationEvents;
using Compliance.Application.Interfaces;
using Compliance.Application.Mappings;
using Compliance.Application.Services;
using Compliance.Domain.Aggregates.BankComplinceAggregate;
using Compliance.Infrastructure.Behaviors;
using Compliance.Infrastructure.ExternalServices.CentralBankCreditCheck;
using Compliance.Infrastructure.ExternalServices.CivilRegistry;
using Compliance.Infrastructure.ExternalServices.PoliceClearance;
using Compliance.Infrastructure.Idempotency;
using Compliance.Infrastructure.IntegrationEvents;
using Compliance.Infrastructure.Persistence;
using Compliance.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Behaviors;
using Shared.EventBus.Abstractions;
using Shared.EventBusRabbitMQ;
using Shared.Infrastructure.Middlewares;
using Shared.IntegrationEventLogEF.Services;
using Shared.ServiceDefaults;

namespace Compliance.API.Extensions
{
    internal static class Extensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            var services = builder.Services;

            builder.AddDefaultAuthentication();

            services.AddDbContext<ComplianceDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")
                ?? throw new InvalidOperationException("Connection string 'ComplianceDbContext' not found.")));

            //services.AddMigration<AccountDbContext, AccountDbContextSeed>();

            services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<ComplianceDbContext>>();

            services.AddTransient<IComplianceIntegrationEventService, ComplianceIntegrationEventService>();

            builder.AddRabbitMqEventBus().AddEventBusSubscriptions();

            services.AddHttpContextAccessor();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateComplianceCommand).Assembly);

                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

            services.AddValidatorsFromAssemblyContaining<ComplianceProfile>();

            services.AddScoped<IComplianceRepository, ComplianceRepository>();
            services.AddScoped<IRequestManager, RequestManager>();
            services.AddScoped<IComplianceService, ComplianceService>();

            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ComplianceProfile).Assembly));
            services.AddCustomCors(builder.Configuration);
            services.AddAuthorizationPolicies();
            services.AddSwaggerDocumentation();
            services.AddRateLimiting(builder.Configuration);
            services.AddCustomHealthChecks(builder.Configuration);

            var configSection = builder.Configuration.GetRequiredSection(BaseUrlConfiguration.CONFIG_NAME);
            services.Configure<BaseUrlConfiguration>(configSection);
            var baseUrlConfig = configSection.Get<BaseUrlConfiguration>();


            services.AddHttpClient<ICivilRegistryService, CivilRegistryClient>(c =>
                c.BaseAddress = new Uri(baseUrlConfig.CivilRegistryBaseAddress));

            services.AddHttpClient<ICentralBankCreditCheckService, CentralBankCreditCheckClient>(c =>
                c.BaseAddress = new Uri(baseUrlConfig.CentralBankCreditCheckBaseAddress));

            services.AddHttpClient<IPoliceClearanceService, PoliceClearanceClient>(c =>
                c.BaseAddress = new Uri(baseUrlConfig.PoliceClearanceBaseAddress));

            services.AddHttpClient(nameof(CentralBankCreditCheckApiHealthCheck), client =>
            {
                client.Timeout = TimeSpan.FromSeconds(5);
            });

            services.AddHttpClient(nameof(CivilRegistryApiHealthCheck), client =>
            {
                client.Timeout = TimeSpan.FromSeconds(5);
            });

            services.AddHttpClient(nameof(PoliceClearanceApiHealthCheck), client =>
            {
                client.Timeout = TimeSpan.FromSeconds(5);
            });

            services.AddHealthChecks()
            .AddCheck<CentralBankCreditCheckApiHealthCheck>("CBCCApiHealthCheck", tags: new[] { "ready" })
            .AddCheck<CivilRegistryApiHealthCheck>("CRApiHealthCheck", tags: new[] { "ready" })
            .AddCheck<PoliceClearanceApiHealthCheck>("PCApiHealthCheck", tags: new[] { "ready" });
        }
        private static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
        {
            //eventBus.AddSubscription<IntegrationEvent, IntegrationEventHandler>();
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
