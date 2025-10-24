using Cli.Commands;
using Cli.Commands.Abstractions;
using Cli.Instructions.Extensions;
using Cli.Workflow;
using Microsoft.Extensions.DependencyInjection;

namespace Cli;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCli<TCliApp>(this IServiceCollection serviceCollection) where TCliApp : CliApp
    {
        serviceCollection.AddConsoleInstructions();
        serviceCollection.AddCommandsFromAssembly(typeof(ExitCommand).Assembly);
        
        serviceCollection.AddSingleton<CliWorkflowCommandProvider>();
        serviceCollection.AddSingleton<CliWorkflow>();

        serviceCollection.AddSingleton<CliIo>();
        serviceCollection.AddSingleton<CliCommandOutcomeIo>();
        
        serviceCollection.AddSingleton<CliApp, TCliApp>();
        
        return serviceCollection;
    }
}