using FusionCache.API.Endpoint.Primitives;
using FusionCache.Domain;
using FusionCache.Models;

namespace FusionCache.API.Endpoint;

public class SampleEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/GetOrders/{partialDescription}",
                async (ISampleService sampleService, string partialDescription) =>
                {
                    return await sampleService.GetOrders(partialDescription);
                }
            )
            .WithName("GetOrders")
            .Produces<IEnumerable<Order>>()
            .WithOpenApi();
    }
}
