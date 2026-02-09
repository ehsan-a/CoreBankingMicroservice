using Account.Application.Interfaces;
using Account.Domain.Aggregates.BankAccountAggregate;
using Account.Infrastructure.Generators;
using Account.Infrastructure.Persistence;
using Account.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Account.API.Extensions.ServiceCollection
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddDbContext<AccountDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Connection")
                ?? throw new InvalidOperationException("Connection string 'AccountDbContext' not found.")));

            services.AddScoped<INumberGenerator, AccountNumberGenerator>();
            //services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.AddScoped<IBankAccountRepository, BankAccountRepository>();

            return services;
        }
    }
}
