using Account.Application.IntegrationEvents;
using Account.Infrastructure.IntegrationEvents;
using Account.Infrastructure.Persistence;
using Shared.EventBus.Abstractions;
using Shared.EventBusRabbitMQ;
using Shared.IntegrationEventLogEF.Services;

namespace Account.API.Extensions.ServiceCollection
{
    internal static class Extensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            var services = builder.Services;


            // Add the integration services that consume the DbContext
            services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<AccountDbContext>>();

            services.AddTransient<IAccountIntegrationEventService, AccountIntegrationEventService>();

            builder.AddRabbitMqEventBus("eventbus")
                   .AddEventBusSubscriptions();

            services.AddHttpContextAccessor();


        }

        private static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
        {
            //eventBus.AddSubscription<GracePeriodConfirmedIntegrationEvent, GracePeriodConfirmedIntegrationEventHandler>();
            //eventBus.AddSubscription<OrderStockConfirmedIntegrationEvent, OrderStockConfirmedIntegrationEventHandler>();
            //eventBus.AddSubscription<OrderStockRejectedIntegrationEvent, OrderStockRejectedIntegrationEventHandler>();
            //eventBus.AddSubscription<OrderPaymentFailedIntegrationEvent, OrderPaymentFailedIntegrationEventHandler>();
            //eventBus.AddSubscription<OrderPaymentSucceededIntegrationEvent, OrderPaymentSucceededIntegrationEventHandler>();
        }
    }

}
