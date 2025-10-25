using System.Reflection;
using Cli.Commands.Abstractions.Extensions;
using Cli.Ynab.Commands.Reporting.SpareMoney;
using Microsoft.Extensions.DependencyInjection;

namespace Cli.Ynab.Commands.Reporting;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReportingCommands(this IServiceCollection serviceCollection)
    {
        var reportingCommandsAssembly = Assembly.GetAssembly(typeof(SpareMoneyCliCommand));
        return serviceCollection.AddCommandsFromAssembly(reportingCommandsAssembly);
    }
}