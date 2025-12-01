using Cli.Commands.Abstractions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using SpendfulnessCli.Aggregation.Aggregator;

namespace SpendfulnessCli.Aggregation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpendfulnessAggregatorCommandArtefacts(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddAggregatorCommandArtefactsFromAssembly(typeof(YnabListAggregator<>).Assembly)
            .AddListAggregatorCommandArtefactsFromAssembly(typeof(YnabListAggregator<>).Assembly);
}