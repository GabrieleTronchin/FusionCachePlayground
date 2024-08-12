namespace FusionCache.API.Endpoint;

using FusionCache.API.Endpoint.Primitives;

public class SampleEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/SampleStreamEntity",
                () =>
                {
                    throw new NotImplementedException();
                }
            )
            .WithName("SampleStreamEntity")
            .Produces<IEnumerable<string>>()
            .WithOpenApi();
    }
}
