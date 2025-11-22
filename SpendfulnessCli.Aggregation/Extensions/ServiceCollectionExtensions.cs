using Cli.Commands.Abstractions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using SpendfulnessCli.Aggregation.Aggregator;

namespace SpendfulnessCli.Aggregation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpendfulnessAggregatorCommandProperties(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddAggregatorCommandPropertiesFromAssembly(typeof(YnabListAggregator<>).Assembly)
            .AddListAggregatorCommandPropertiesFromAssembly(typeof(YnabListAggregator<>).Assembly);
}