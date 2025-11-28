using Microsoft.Extensions.DependencyInjection;
using Splitwise.Http;

namespace Splitwise;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSplitwise(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddHttpClient()
            .AddSingleton<SplitwiseHttpClientBuilder>();
}