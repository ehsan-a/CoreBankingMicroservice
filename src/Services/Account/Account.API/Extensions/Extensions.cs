using Account.Application.Commands;
using Account.Application.IntegrationEvents;
using Account.Application.Interfaces;
using Account.Application.Services;
using Account.Domain.Aggregates.BankAccountAggregate;
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
using Shared.EventBusRabbitMQ;
using Shared.Infrastructure.Middlewares;
using Shared.IntegrationEventLogEF.Services;
using Shared.ServiceDefaults;
using System.Reflection;

namespace Account.API.Extensions
{
    internal static class Extensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            var services = builder.Services;

            services.AddMediatR(cfg =>
            {
                //cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
                cfg.RegisterServicesFromAssembly(typeof(CreateBankAccountCommand).Assembly);
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.Load("Account.Application"));

            services.AddDbContext<AccountDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")
                ?? throw new InvalidOperationException("Connection string 'AccountDbContext' not found.")));

            services.AddScoped<INumberGenerator, AccountNumberGenerator>();
            //services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.AddScoped<IBankAccountRepository, BankAccountRepository>();

            services.AddScoped<IRequestManager, RequestManager>();

            services.AddAutoMapper(cfg =>
cfg.AddMaps(typeof(CreateBankAccountCommand).Assembly));

            services.AddScoped<IBankAccountService, BankAccountService>();

            services.AddCustomCors(builder.Configuration);

            builder.AddDefaultAuthentication();

            services.AddAuthorizationPolicies();

            services.AddSwaggerDocumentation();
            services.AddRateLimiting(builder.Configuration);
            services.AddCustomHealthChecks(builder.Configuration);


            // Add the integration services that consume the DbContext
            services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<AccountDbContext>>();

            services.AddTransient<IAccountIntegrationEventService, AccountIntegrationEventService>();

            builder.AddRabbitMqEventBus("eventbus")
                   .AddEventBusSubscriptions();

            services.AddHttpContextAccessor();


        }

        private static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
        {
            //eventBus.AddSubscription<AccountCreatedIntegrationEvent, GracePeriodConfirmedIntegrationEventHandler>();
            //eventBus.AddSubscription<OrderStockConfirmedIntegrationEvent, OrderStockConfirmedIntegrationEventHandler>();
            //eventBus.AddSubscription<OrderStockRejectedIntegrationEvent, OrderStockRejectedIntegrationEventHandler>();
            //eventBus.AddSubscription<OrderPaymentFailedIntegrationEvent, OrderPaymentFailedIntegrationEventHandler>();
            //eventBus.AddSubscription<OrderPaymentSucceededIntegrationEvent, OrderPaymentSucceededIntegrationEventHandler>();
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
