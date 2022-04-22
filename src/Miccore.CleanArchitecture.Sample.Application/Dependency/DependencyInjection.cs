using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace Miccore.CleanArchitecture.Sample.Application.Dependency
{
    /// <summary>
    /// adding mediatr commandes
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// add mediatr commands and query assembly
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddMediat(this IServiceCollection services, IConfiguration configuration){

            #region addMediatR
                services.AddMediatR(Assembly.GetExecutingAssembly());
            #endregion
            
            return services;
        }
    }
}