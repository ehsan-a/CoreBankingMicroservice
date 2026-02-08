using FluentValidation;
using System.Reflection;

namespace Account.API.Extensions.ServiceCollection
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddValidation(
            this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.Load("Account.Application"));

            return services;
        }
    }
}
