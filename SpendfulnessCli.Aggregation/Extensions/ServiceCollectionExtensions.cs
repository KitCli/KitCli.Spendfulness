using System.Reflection;
using Cli.Commands.Abstractions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using SpendfulnessCli.Aggregation.Aggregator.ListAggregators;

namespace SpendfulnessCli.Aggregation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAggregateCommandProperties(this IServiceCollection serviceCollection)
    {
        var reportingCommandsAssembly = Assembly.GetAssembly(typeof(ListYnabAggregator<>));
        return serviceCollection
            .AddCommandPropertiesFromAssembly(reportingCommandsAssembly);
    }
}