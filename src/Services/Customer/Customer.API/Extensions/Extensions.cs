using CoreBanking.Infrastructure.Repositories;
using Customer.Application.Commands;
using Customer.Application.IntegrationEvents;
using Customer.Application.Interfaces;
using Customer.Application.Mappings;
using Customer.Application.Services;
using Customer.Domain.Aggregates.BankCustomerAggregate;
using Customer.Infrastructure.Behaviors;
using Customer.Infrastructure.Idempotency;
using Customer.Infrastructure.IntegrationEvents;
using Customer.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Behaviors;
using Shared.EventBus.Abstractions;
using Shared.EventBusRabbitMQ;
using Shared.Infrastructure.Middlewares;
using Shared.IntegrationEventLogEF.Services;
using Shared.ServiceDefaults;

namespace Customer.API.Extensions
{
    internal static class Extensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            var services = builder.Services;

            builder.AddDefaultAuthentication();

            services.AddDbContext<CustomerDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")
                ?? throw new InvalidOperationException("Connection string 'CustomerDbContext' not found.")));

            //services.AddMigration<AccountDbContext, AccountDbContextSeed>();

            services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<CustomerDbContext>>();

            services.AddTransient<ICustomerIntegrationEventService, CustomerIntegrationEventService>();

            builder.AddRabbitMqEventBus("eventbus")
                   .AddEventBusSubscriptions();

            services.AddHttpContextAccessor();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateBankCustomerCommand).Assembly);

                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

            services.AddValidatorsFromAssemblyContaining<IdentifiedCommandValidator>();

            services.AddScoped<IBankCustomerRepository, BankCustomerRepository>();
            services.AddScoped<IRequestManager, RequestManager>();
            services.AddScoped<IBankCustomerService, BankCustomerService>();

            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(CustomerProfile).Assembly));
            services.AddCustomCors(builder.Configuration);
            services.AddAuthorizationPolicies();
            services.AddSwaggerDocumentation();
            services.AddRateLimiting(builder.Configuration);
            services.AddCustomHealthChecks(builder.Configuration);
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
