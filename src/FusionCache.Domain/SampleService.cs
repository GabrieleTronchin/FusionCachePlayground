using Bogus;
using FusionCache.Models;
using ZiggyCreatures.Caching.Fusion;

namespace FusionCache.Domain;

public class SampleService(IFusionCache cache) : ISampleService
{

    public async Task<IEnumerable<Order>> GetOrders(string partialDescription)
    {
        var orders = await cache.GetOrSetAsync($"{nameof(SampleService)}-{partialDescription}",
             new Faker<Order>()
                .RuleFor(nameof(Order.Description), f => $"{partialDescription}-{f.Random.String2(10)}")
                .RuleFor(nameof(Order.Id), f => f.Random.Int())
                .RuleFor(nameof(Order.Quantity), f => f.Random.Int())
                .GenerateBetween(5, 15)
            );

        return orders;
    }

}
