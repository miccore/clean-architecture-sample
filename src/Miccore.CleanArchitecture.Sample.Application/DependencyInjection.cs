using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Miccore.CleanArchitecture.Sample.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAllHandlers(
            this IServiceCollection services,
            ServiceLifetime withLifetime = ServiceLifetime.Transient
        )
        {
            return services.Scan(scan => scan
                .FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(classes =>
                    classes.AssignableTo(typeof(IRequestHandler<>))
                        .Where(c => !c.IsAbstract && !c.IsGenericTypeDefinition))
                .AsSelfWithInterfaces()
                .WithLifetime(withLifetime)
            );
        }
    }
}