using Account.Application.Commands;
using Account.Application.Interfaces;
using Account.Application.Services;
using Account.Infrastructure.Persistence;
using Shared.EventBus.Abstractions;
using Shared.EventBus.Extensions;
using Shared.IntegrationEventLogEF.Services;

namespace Account.API.Extensions.ServiceCollection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(
             this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
      cfg.AddMaps(typeof(CreateBankAccountCommand).Assembly));

            services.AddScoped<IBankAccountService, BankAccountService>();





            return services;
        }

    }
}
