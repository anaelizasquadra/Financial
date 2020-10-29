using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Financial.Services
{
    public static class Bootstrapper
    {
        public static IServiceCollection UseServicesHandlers(this IServiceCollection services)
        {
            return services
                .AddMediatR(typeof(Bootstrapper).Assembly);
        }
    }
}
