using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Behaviors;
using Shared.EventBus.Abstractions;
using Shared.EventBusRabbitMQ;
using Shared.Infrastructure.Middlewares;
using Shared.IntegrationEventLogEF.Services;
using Shared.ServiceDefaults;
using Transaction.Application.Commands;
using Transaction.Application.IntegrationEvents;
using Transaction.Application.Interfaces;
using Transaction.Application.Mappings;
using Transaction.Application.Services;
using Transaction.Domain.Aggregates.AccountTransactionAggregate;
using Transaction.Infrastructure.Behaviors;
using Transaction.Infrastructure.Idempotency;
using Transaction.Infrastructure.IntegrationEvents;
using Transaction.Infrastructure.Persistence;
using Transaction.Infrastructure.Repositories;

namespace Transaction.API.Extensions
{
    internal static class Extensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            var services = builder.Services;

            builder.AddDefaultAuthentication();

            services.AddDbContext<TransactionDbContext>(options =>
                options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("Connection")
                ?? throw new InvalidOperationException("Connection string 'TransactionDbContext' not found.")));

            //services.AddMigration<AccountDbContext, AccountDbContextSeed>();

            services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<TransactionDbContext>>();

            services.AddTransient<ITransactionIntegrationEventService, TransactionIntegrationEventService>();

            builder.AddRabbitMqEventBus()
                   .AddEventBusSubscriptions();

            services.AddHttpContextAccessor();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateTransactionCommand).Assembly);

                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

            services.AddValidatorsFromAssemblyContaining<IdentifiedCommandValidator>();

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IRequestManager, RequestManager>();
            services.AddScoped<ITransactionService, TransactionService>();

            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(TransactionProfile).Assembly));
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
