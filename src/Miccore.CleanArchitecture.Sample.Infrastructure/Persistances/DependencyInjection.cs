using Miccore.CleanArchitecture.Sample.Core.Repositories;
using Miccore.CleanArchitecture.Sample.Core.Repositories.Base;
using Miccore.CleanArchitecture.Sample.Infrastructure.Data;
using Miccore.CleanArchitecture.Sample.Infrastructure.Repositories;
using Miccore.CleanArchitecture.Sample.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Miccore.CleanArchitecture.Sample.Infrastructure.Persistances
{
    /// <summary>
    /// dependency injection of database injection
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// add db context
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            // get connection string from environment file
            var connectionString = configuration.GetConnectionString("SampleDB");

            // database connexion
            services.AddDbContext<SampleApplicationDbContext>(option =>
            {
                option.UseMySql(connectionString, new MySqlServerVersion(new Version()));
            }, ServiceLifetime.Scoped);

            // add repositories
            #region repositories

            services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.TryAddScoped<ISampleRepository, SampleRepository>();

            #endregion

            return services;
        }

    }
}