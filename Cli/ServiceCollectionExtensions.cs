using Cli.Commands.Abstractions.Io;
using Cli.Commands.Abstractions.Io.Outcomes;
using Cli.Instructions.Extensions;
using Cli.Workflow;
using Cli.Workflow.Abstractions;
using Cli.Workflow.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Cli;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCli<TCliApp>(this IServiceCollection serviceCollection) where TCliApp : CliApp
    {
        serviceCollection.AddCliInstructions();
        
        serviceCollection.AddCliWorkflowCommands();
        
        serviceCollection.AddSingleton<ICliWorkflow, CliWorkflow>();

        serviceCollection.AddSingleton<CliIo>();
        serviceCollection.AddSingleton<CliCommandOutcomeIo>();
        
        serviceCollection.AddSingleton<CliApp, TCliApp>();
        
        return serviceCollection;
    }
}