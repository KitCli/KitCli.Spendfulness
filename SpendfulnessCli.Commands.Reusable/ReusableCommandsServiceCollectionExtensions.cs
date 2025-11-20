using System.Reflection;
using Cli.Commands.Abstractions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using SpendfulnessCli.Commands.Reusable.Table;

namespace SpendfulnessCli.Commands.Reusable;

public static class ReusableCommandsServiceCollectionExtensions
{
    public static IServiceCollection AddReusableCommands(this IServiceCollection serviceCollection)
    {
        var reportingCommandsAssembly = Assembly.GetAssembly(typeof(TableCliCommand));
        return serviceCollection.AddCommandsFromAssembly(reportingCommandsAssembly);
    }
}