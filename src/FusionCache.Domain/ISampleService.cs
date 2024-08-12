using FusionCache.Models;

namespace FusionCache.Domain
{
    public interface ISampleService
    {
        Task<Order?> GetOrder(int id);
        Task<IEnumerable<Order>> GetOrders(string partialDescription);
        Task<IEnumerable<Order>> GetOrdersWithFailSafe(string partialDescription);
    }
}