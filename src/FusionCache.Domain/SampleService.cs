using Bogus;
using FusionCache.Models;
using ZiggyCreatures.Caching.Fusion;

namespace FusionCache.Domain;

public class SampleService(IFusionCache cache) : ISampleService
{

    /// <summary>
    /// Most simple use of fusion cache
    /// </summary>
    /// <param name="partialDescription"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Order>> GetOrders(string partialDescription)
    {
        var orders = await cache.GetOrSetAsync($"{nameof(SampleService)}-{partialDescription}",
               await GetOrdersFromDatabase(partialDescription)
            );

        return orders;
    }

    /// <summary>
    /// This method simulate a slow data retrival operation.

    /// </summary>
    /// <param name="partialDescription"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Order>> GetOrdersWithFailSafe(string partialDescription)
    {
        return await cache.GetOrSetAsync<IEnumerable<Order>>($"{nameof(SampleService)}-{partialDescription}",
               async (ctx, ct) =>
               {
                   await Task.Delay(1000);
                   return await GetOrdersFromDatabase(partialDescription);
               },
                options => options
                .SetDuration(TimeSpan.FromMinutes(2)) // entity stay in cache for 2 minutes then it expire
                .SetFailSafe(true) //enable fail safe
                .SetFactoryTimeouts(TimeSpan.FromMilliseconds(100)) // if after 2 min someone try to retrive an entity and operation takes more than 100ms it use the exprire data if exist
            );

    }


    /// <summary>
    /// Adaptive caching sample, we can decide cache duration dependeing from entity parameters
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Order?> GetOrder(int id)
    {
        return await cache.GetOrSetAsync<Order?>(
                     $"{nameof(SampleService)}-{id}",
                       async (ctx, ct) =>
                       {
                           var order = await GetOrderFromDatabase(id);
                           //Checking LastTime Data update time to determinate cache time
                           if (order is null)
                           {
                               ctx.Options.Duration = TimeSpan.FromMinutes(5);
                           }
                           else if (order.LastUpdateTime > DateTime.UtcNow.AddDays(-1))
                           {
                               ctx.Options.Duration = TimeSpan.FromMinutes(1);
                           }
                           else if (order.LastUpdateTime > DateTime.UtcNow.AddDays(-10))
                           {
                               ctx.Options.Duration = TimeSpan.FromMinutes(10);
                           }
                           else
                           {
                               ctx.Options.Duration = TimeSpan.FromMinutes(30);
                           }

                           return order;
                       },
                     options => options.SetDuration(TimeSpan.FromMinutes(1)) //Default Cache duration
                 );

    }


    private async Task<Order> GetOrderFromDatabase(int id)
    {
        return await Task.FromResult(new Faker<Order>()
                .RuleFor(nameof(Order.Description), f => $"{id}-{f.Random.String2(10)}")
                .RuleFor(nameof(Order.Id), f => id)
                .RuleFor(nameof(Order.Quantity), f => f.Random.Int())
                .RuleFor(nameof(Order.LastUpdateTime), f => f.Date.RecentOffset()));

    }

    private async Task<IEnumerable<Order>> GetOrdersFromDatabase(string partialDescription)
    {
        return await Task.FromResult(new Faker<Order>()
                .RuleFor(nameof(Order.Description), f => $"{partialDescription}-{f.Random.String2(10)}")
                .RuleFor(nameof(Order.Id), f => f.Random.Int())
                .RuleFor(nameof(Order.Quantity), f => f.Random.Int())
                .RuleFor(nameof(Order.Quantity), f => f.Random.Int())
                .RuleFor(nameof(Order.LastUpdateTime), f => f.Date.RecentOffset())
                .GenerateBetween(5, 15));

    }


}
