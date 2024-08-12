# HybridCache Playground

In .NET, there are currently two primary ways to implement caching:

- **Memory Cache**: A local cache stored in RAM, providing fast access to frequently used data.
- **Distributed Cache**: A remote cache stored in an external database such as Redis, Garnet, or SQL, which is useful for sharing cache data across multiple instances of an application.

With the upcoming release of .NET 9, a new caching mechanism called **HybridCache** is set to be introduced.

**HybridCache** combines the benefits of both memory and distributed caches, allowing it to provide the speed of an in-memory cache while also offering the durability and scalability of a distributed or persistent cache. 

This makes it particularly useful for applications that need both performance and reliability in their caching strategy.

One of the key features of HybridCache is that it is designed to be a drop-in replacement for existing implementations of `IDistributedCache` and `IMemoryCache`.

If an application already has an `IDistributedCache` implementation, the HybridCache service can leverage it for secondary caching, ensuring that cached data is always available even if one cache layer fails.

### Solving Cache Stampede with HybridCache

HybridCache also addresses a common issue faced when using the plain `IDistributedCache` interface: **Cache Stampede**.

## What is Cache Stampede?

Cache stampede occurs when a frequently accessed cache entry expires, and multiple requests attempt to repopulate the cache simultaneously. This can lead to significant performance degradation as all the requests hit the underlying data store at once, negating the benefits of caching.

In a high-load scenario, a cache stampede typically unfolds as follows:

1. Multiple requests arrive at the same time, all seeking the same data.
2. All requests check the cache for the data.
3. None of the requests find the data in the cache because the cache entry has expired or was never populated.
4. All requests then proceed to the database (or another data source) to retrieve the same piece of data.
5. Finally, each request attempts to save this newly retrieved data back into the cache, often resulting in redundant operations.

This simultaneous fetching and caching by multiple requests can overload the database, cause unnecessary latency, and waste resources.

HybridCache mitigates this issue by implementing strategies such as "locking" or "single flight" mechanisms, where only one request repopulates the cache while others wait for the data to be refreshed. This ensures that the system remains performant even under heavy load.

### Additional Resources on Cache Stampede:

- [Wikipedia: Cache Stampede](https://en.wikipedia.org/wiki/Cache_stampede)
- [Microsoft Documentation: Hybrid Caching in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/hybrid?view=aspnetcore-9.0)
- [GeeksforGeeks: Cache Stampede or Dogpile Problem in System Design](https://www.geeksforgeeks.org/cache-stempede-or-dogpile-problem-in-system-design/)
