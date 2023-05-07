using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WebShop.Application.Behaviors;
using WebShop.Application.Common.Dates;
using WebShop.Application.Contracts.Services;
using WebShop.Application.Features.ShoppingCartItems.Commands.CreateShoppingCartItem.StockServices;

namespace WebShop.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
            services.AddScoped(typeof(IDateService), typeof(DateService));
            services.AddScoped(typeof(IStockService), typeof(StockService));

            return services;
        }
    }
}
