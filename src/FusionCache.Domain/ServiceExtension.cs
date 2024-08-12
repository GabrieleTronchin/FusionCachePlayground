using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FusionCache.Domain;

public static class ServiceExtension
{
    public static IServiceCollection AddDomain(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddFusionCache();

        services.AddFusionCacheStackExchangeRedisBackplane(options => configuration.Bind("DistributedCache", options));

        services.AddTransient<ISampleService, SampleService>();


        return services;
    }


}
