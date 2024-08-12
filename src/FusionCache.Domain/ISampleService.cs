using FusionCache.Models;

namespace FusionCache.Domain
{
    public interface ISampleService
    {
        Task<IEnumerable<Order>> GetOrders(string partialDescription);
    }
}