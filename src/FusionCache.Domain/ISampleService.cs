using FusionCache.Models;

namespace FusionCache.Domain
{
    public interface ISampleService
    {
        Task<Order?> GetOrder(int id);
        Task<IEnumerable<Order>> GetOrders();
        Task<IEnumerable<Order>> GetOrdersByDescriptionEagerRefresh(string partialDescription);
        Task<IEnumerable<Order>> GetOrdersByDescriptionWithFailSafe(string partialDescription);
    }
}
