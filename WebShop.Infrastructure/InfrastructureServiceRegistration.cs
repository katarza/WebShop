using Microsoft.Extensions.DependencyInjection;
using WebShop.Application.Contracts.Infrastructure;
using WebShop.Infrastructure.SupplierStock;

namespace WebShop.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {

            services.AddTransient<ISupplierStockService, SupplierStockService>();

            return services;
        }
    }
}
