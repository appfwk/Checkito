using Checkito.Data.Apis.Queries;
using Checkito.Data.Infrastructure.Databases;
using Checkito.Testing.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Checkito.Infrastructure.Configuration
{
    public static class CheckitoConfigExtensions
    {

        public static IServiceCollection RegisterAllInterfaces<TImplementation>(this IServiceCollection services)
        {
            return services.Scan(scan => scan
                .FromAssemblyOf<TImplementation>()
                .AddClasses()
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

        public static IServiceCollection AddCheckito(this IServiceCollection services)
        {
            services.RegisterAllInterfaces<TestRunner>();
            services.RegisterAllInterfaces<IDatabase>();
            //services.AddScoped<IDatabase, FileSystemDatabase>();
            services.AddMediatR(typeof(GetApisQuery));

            return services;
        }
    }
}
