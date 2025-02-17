using Microsoft.Extensions.DependencyInjection;
using Ynab.Clients;
using Ynab.Http;

namespace Ynab.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddYnab(this IServiceCollection serviceCollection) 
        => serviceCollection
            .AddHttpClient()
            .AddSingleton<YnabHttpClientFactory>()
            .AddSingleton<BudgetsClient>();
}