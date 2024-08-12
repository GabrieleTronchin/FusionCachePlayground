using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZiggyCreatures.Caching.Fusion.Backplane.StackExchangeRedis;
using ZiggyCreatures.Caching.Fusion.Serialization.NewtonsoftJson;
using ZiggyCreatures.Caching.Fusion;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace FusionCache.Domain;

public static class ServiceExtension
{
    public static IServiceCollection AddDomain(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        string redisConnectionString = configuration.GetSection("DistributedCache:Configuration").Value 
            ?? throw new InvalidDataException("Missing DistributedCache Configuration");

        services.AddFusionCache()
            .WithSerializer(
                new FusionCacheNewtonsoftJsonSerializer()
            ).WithDistributedCache(
                new RedisCache(new RedisCacheOptions { Configuration = redisConnectionString })
            )
            .WithBackplane(
                new RedisBackplane(new RedisBackplaneOptions { Configuration = redisConnectionString })
            );

        services.AddTransient<ISampleService, SampleService>();

        return services;
    }


}
