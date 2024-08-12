var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddStackExchangeRedisCache(options => builder.Configuration.Bind("RedisCache", options));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapGet(
//        "/GetOrCreateAsync/{key}",
//        async (string key, HybridCache cache, CancellationToken token = default) =>
//        await cache.GetOrCreateAsync(
//                key,
//                async (token) => await Task.Run(() => $"{nameof(cache.GetOrCreateAsync)} - Hello World - {DateTime.Now}"),
//                token: token
//            )
//    )
//    .WithName("GetOrCreateAsync")
//    .WithOpenApi();



app.Run();
