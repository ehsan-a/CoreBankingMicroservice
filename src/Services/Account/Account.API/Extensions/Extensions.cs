using Account.Application.Commands;
using Account.Application.IntegrationEvents;
using Account.Application.IntegrationEvents.EventHandling;
using Account.Application.IntegrationEvents.Events;
using Account.Application.Interfaces;
using Account.Application.Services;
using Account.Domain.Aggregates.BankAccountAggregate;
using Account.Domain.Replicas;
using Account.Infrastructure.Behaviors;
using Account.Infrastructure.Generators;
using Account.Infrastructure.Idempotency;
using Account.Infrastructure.IntegrationEvents;
using Account.Infrastructure.Persistence;
using Account.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Behaviors;
using Shared.EventBus.Abstractions;
using Shared.EventBus.Extensions;
using Shared.EventBusRabbitMQ;
using Shared.Infrastructure.Middlewares;
using Shared.IntegrationEventLogEF.Services;
using Shared.ServiceDefaults;

namespace Account.API.Extensions
{
    internal static class Extensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            var services = builder.Services;

            builder.AddDefaultAuthentication();

            services.AddDbContext<AccountDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")
                ?? throw new InvalidOperationException("Connection string 'AccountDbContext' not found.")));

            //services.AddMigration<AccountDbContext, AccountDbContextSeed>();

            services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<AccountDbContext>>();

            services.AddTransient<IAccountIntegrationEventService, AccountIntegrationEventService>();

            builder.AddRabbitMqEventBus()
                   .AddEventBusSubscriptions();

            services.AddHttpContextAccessor();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateBankAccountCommand).Assembly);

                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

            services.AddValidatorsFromAssemblyContaining<IdentifiedCommandValidator>();

            services.AddScoped<INumberGenerator, AccountNumberGenerator>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<ICustomerReplicaRepository, CustomerReplicaRepository>();
            services.AddScoped<IRequestManager, RequestManager>();
            services.AddScoped<IBankAccountService, BankAccountService>();

            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(CreateBankAccountCommand).Assembly));
            services.AddCustomCors(builder.Configuration);
            services.AddAuthorizationPolicies();
            services.AddSwaggerDocumentation();
            services.AddRateLimiting(builder.Configuration);
            services.AddCustomHealthChecks(builder.Configuration);
        }

        private static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
        {
            eventBus.AddSubscription<TransactionCreatedIntegrationEvent, TransactionCreatedIntegrationEventHandler>();
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
