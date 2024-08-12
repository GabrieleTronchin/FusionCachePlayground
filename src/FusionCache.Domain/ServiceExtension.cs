using FusionCache.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FusionCache.Domain;

public static class ServiceExtension
{
    public static IServiceCollection AddFusionCache(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddFusionCache();

        services.AddFusionCacheStackExchangeRedisBackplane(options => configuration.Bind("DistributedCache", options));

        return services;
    }


}
