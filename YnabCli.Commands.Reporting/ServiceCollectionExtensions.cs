using System.Reflection;
using Cli.Commands.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using YnabCli.Commands.Reporting.SpareMoney;

namespace YnabCli.Commands.Reporting;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReportingCommands(this IServiceCollection serviceCollection)
    {
        var reportingCommandsAssembly = Assembly.GetAssembly(typeof(SpareMoneyCommand));
        return serviceCollection.AddCommandsFromAssembly(reportingCommandsAssembly);
    }
}