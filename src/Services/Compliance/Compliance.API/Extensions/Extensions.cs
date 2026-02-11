//using Compliance.API.Extensions;
//using CoreBanking.Infrastructure.Repositories;
//using Customer.API.HealthChecks;
//using Customer.Application;
//using Customer.Application.Commands;
//using Customer.Application.IntegrationEvents;
//using Customer.Application.Interfaces;
//using Customer.Application.Services;
//using Customer.Domain.Aggregates.BankCustomerAggregate;
//using Customer.Infrastructure.Behaviors;
//using Customer.Infrastructure.ExternalServices.CentralBankCreditCheck;
//using Customer.Infrastructure.ExternalServices.CivilRegistry;
//using Customer.Infrastructure.ExternalServices.PoliceClearance;
//using Customer.Infrastructure.Idempotency;
//using Customer.Infrastructure.IntegrationEvents;
//using Customer.Infrastructure.Persistence;
//using FluentValidation;
//using Microsoft.EntityFrameworkCore;
//using Shared.Application.Behaviors;
//using Shared.EventBus.Abstractions;
//using Shared.EventBusRabbitMQ;
//using Shared.Infrastructure.Middlewares;
//using Shared.IntegrationEventLogEF.Services;
//using Shared.ServiceDefaults;

//namespace Compliance.API.Extensions
//{
//    internal static class Extensions
//    {
//        public static void AddApplicationServices(this IHostApplicationBuilder builder)
//        {
           

//            var configSection = builder.Configuration.GetRequiredSection(BaseUrlConfiguration.CONFIG_NAME);
//            services.Configure<BaseUrlConfiguration>(configSection);
//            var baseUrlConfig = configSection.Get<BaseUrlConfiguration>();


//            services.AddHttpClient<ICivilRegistryService, CivilRegistryClient>(c =>
//                c.BaseAddress = new Uri(baseUrlConfig.CivilRegistryBaseAddress));

//            services.AddHttpClient<ICentralBankCreditCheckService, CentralBankCreditCheckClient>(c =>
//                c.BaseAddress = new Uri(baseUrlConfig.CentralBankCreditCheckBaseAddress));

//            services.AddHttpClient<IPoliceClearanceService, PoliceClearanceClient>(c =>
//                c.BaseAddress = new Uri(baseUrlConfig.PoliceClearanceBaseAddress));

//            services.AddHttpClient(nameof(CentralBankCreditCheckApiHealthCheck), client =>
//            {
//                client.Timeout = TimeSpan.FromSeconds(5);
//            });

//            services.AddHttpClient(nameof(CivilRegistryApiHealthCheck), client =>
//            {
//                client.Timeout = TimeSpan.FromSeconds(5);
//            });

//            services.AddHttpClient(nameof(PoliceClearanceApiHealthCheck), client =>
//            {
//                client.Timeout = TimeSpan.FromSeconds(5);
//            });

//            services.AddHealthChecks()
//            .AddCheck<CentralBankCreditCheckApiHealthCheck>("CBCCApiHealthCheck", tags: new[] { "ready" })
//            .AddCheck<CivilRegistryApiHealthCheck>("CRApiHealthCheck", tags: new[] { "ready" })
//            .AddCheck<PoliceClearanceApiHealthCheck>("PCApiHealthCheck", tags: new[] { "ready" });
//        }

       
//    }

//}
