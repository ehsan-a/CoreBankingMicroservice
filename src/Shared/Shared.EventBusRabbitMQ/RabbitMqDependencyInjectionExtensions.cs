using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.EventBus.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.EventBusRabbitMQ
{
    public static class RabbitMqDependencyInjectionExtensions
    {
        // {
        //   "EventBus": {
        //     "SubscriptionClientName": "...",
        //     "RetryCount": 10
        //   }
        // }

        private const string SectionName = "EventBus";
        private const string RabbitMqSectionName = "RabbitMQ";

        public static IEventBusBuilder AddRabbitMqEventBus(this IHostApplicationBuilder builder, string connectionName)
        {
            ArgumentNullException.ThrowIfNull(builder);

            //builder.AddRabbitMQClient(connectionName);

            // Options support
            builder.Services.Configure<EventBusOptions>(builder.Configuration.GetSection(SectionName));

            builder.Services.Configure<RabbitMqOptions>(
     builder.Configuration.GetSection(RabbitMqSectionName));

            // Abstractions on top of the core client API
            builder.Services.AddSingleton<IEventBus, RabbitMQEventBus>();
            // Start consuming messages as soon as the application starts
            builder.Services.AddSingleton<IHostedService>(sp => (RabbitMQEventBus)sp.GetRequiredService<IEventBus>());



            return new EventBusBuilder(builder.Services);
        }

        private class EventBusBuilder(IServiceCollection services) : IEventBusBuilder
        {
            public IServiceCollection Services => services;
        }
    }
}
