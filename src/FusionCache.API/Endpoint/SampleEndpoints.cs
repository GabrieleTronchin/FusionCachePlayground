using FusionCache.API.Endpoint.Primitives;
using FusionCache.Domain;
using FusionCache.Models;

namespace FusionCache.API.Endpoint;

public class SampleEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/GetAllOrders",
                async (ISampleService sampleService) =>
                {
                    return await sampleService.GetOrders();
                }
            )
            .WithName("GetAllOrders")
            .Produces<IEnumerable<Order>>()
            .WithOpenApi();

        app.MapGet(
                "/GetOrdersByDesciptionEagerRefresh/{partialDescription}",
                async (ISampleService sampleService, string partialDescription) =>
                {
                    return await sampleService.GetOrdersByDesciptionEagerRefresh(
                        partialDescription
                    );
                }
            )
            .WithName("GetOrdersByDesciptionEagerRefresh")
            .Produces<IEnumerable<Order>>()
            .WithOpenApi();

        app.MapGet(
                "/GetOrdersByDesciptionWithFailSafe/{partialDescription}",
                async (ISampleService sampleService, string partialDescription) =>
                {
                    return await sampleService.GetOrdersByDesciptionWithFailSafe(
                        partialDescription
                    );
                }
            )
            .WithName("GetOrdersByDesciptionWithFailSafe")
            .Produces<IEnumerable<Order>>()
            .WithOpenApi();

        app.MapGet(
                "/GetOrder/{Id}",
                async (ISampleService sampleService, int Id) =>
                {
                    return await sampleService.GetOrder(Id);
                }
            )
            .WithName("GetOrder")
            .Produces<IEnumerable<Order>>()
            .WithOpenApi();
    }
}
