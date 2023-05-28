
using Account.Application.Features.Account.Queries;
using Account.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Account.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<GetAccountRequest, List<UserAccount>>, GetAccountHandler>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
