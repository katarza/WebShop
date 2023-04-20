using Microsoft.Extensions.DependencyInjection;
using WebShop.Application.Contracts.Infrastructure;
using WebShop.Application.Models.SuppliersService;
using WebShop.Infrastructure.SupplierStock;
using Microsoft.Extensions.Configuration;

namespace WebShop.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<SuppliersServiceSettings>(configuration.GetSection("SuppliersServiceSettings"));
            services.AddTransient<ISupplierStockService, SupplierStockService>();

            return services;
        }
    }
}
