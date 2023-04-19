using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Contracts.Persistence;
using WebShop.Persistence.Repository;

namespace WebShop.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<WebShopDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "WebShopDb"));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IShoppingCartItemRepository), typeof(ShoppingCartItemsRepository));
            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));

            return services;
        }
    }
}
