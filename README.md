# Fusion Cache Playground

This repository serves as a playground for testing and experimenting with FusionCache, a highly efficient and versatile caching library for .NET. FusionCache simplifies the caching process by offering several advanced features out of the box, making it one of the best options available.

### Key Features of FusionCache:
- **Hybrid Cache Implementation**: Combines the benefits of `IMemoryCache` for in-memory caching and `IDistributedCache` for distributed caching, allowing you to leverage both local and remote cache storage seamlessly.
- **Cache Stampede Prevention**: Implements mechanisms to prevent cache stampedes, ensuring that the system remains performant even under heavy load.
- **In-Memory Cache Synchronization**: Automatically synchronizes the in-memory cache across instances when scaling, using Redis notifications. This feature is particularly useful in distributed environments to ensure consistency and reduce the risk of cache coherence issues.

For more detailed information, refer to the official FusionCache documentation:
[Official Documentation](https://github.com/ZiggyCreatures/FusionCache)