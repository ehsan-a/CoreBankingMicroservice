using Account.Application.Commands;
using Account.Application.Interfaces;
using Account.Application.Services;
using Microsoft.AspNetCore.Authentication;

namespace Account.API.Extensions.ServiceCollection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(
             this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
      cfg.AddMaps(typeof(CreateAccountCommand).Assembly));

            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
