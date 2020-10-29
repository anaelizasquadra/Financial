using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Financial.Infrastructure
{
    public static class Bootstrapper
    {
        public static IServiceCollection UseFinancialDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDbContext<FinancialDbContext>(config => config.UseSqlServer(configuration.GetConnectionString("FinancialDbContext")));
        }
    }
}
