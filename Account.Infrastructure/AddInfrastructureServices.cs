
using Microsoft.Extensions.DependencyInjection;

using Account.Infrastructure.Contracts;
using Account.Infrastructure.Repositories;

namespace Account.Infrastructure
{
    public static class AddInfrastructureServices
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
        {

            services.AddTransient<IAccountRepository, AccountRepository>();

            return services;

        }
    }
}
