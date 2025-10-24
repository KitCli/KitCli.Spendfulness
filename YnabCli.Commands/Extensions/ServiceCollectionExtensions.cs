using System.Reflection;
using Cli.Commands.Abstractions;
using Cli.Instructions;
using Microsoft.Extensions.DependencyInjection;
using YnabCli.Commands.Builders;
using YnabCli.Commands.Generators;
using YnabCli.Database;

namespace YnabCli.Commands.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommands(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<ConfiguredBudgetClient>()
            .AddSingleton<CommandHelpCliTableBuilder>();
    }
}