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


        var options = new FusionCacheOptions()
        {
            DefaultEntryOptions = new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromMinutes(1),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromHours(2),
                FailSafeThrottleDuration = TimeSpan.FromSeconds(30),
                DistributedCacheDuration = TimeSpan.FromMinutes(1)
            }
        };


        services.AddFusionCache()
            .WithSerializer(
                new FusionCacheNewtonsoftJsonSerializer()
            ).WithDistributedCache(
                new RedisCache(new RedisCacheOptions { Configuration = redisConnectionString })
            )
            .WithBackplane(
                new RedisBackplane(new RedisBackplaneOptions { Configuration = redisConnectionString })
            )
            .WithOptions(options);



        services.AddTransient<ISampleService, SampleService>();

        return services;
    }


}
