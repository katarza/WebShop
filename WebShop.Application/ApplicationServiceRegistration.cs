using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WebShop.Application.Behaviors;

namespace WebShop.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            var applicationAssembly = typeof(AssemblyReference).Assembly;
            services.AddValidatorsFromAssembly(applicationAssembly);

            return services;
        }
    }
}
