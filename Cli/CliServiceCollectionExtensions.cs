using Cli.Abstractions;
using Cli.Abstractions.Io;
using Cli.Commands.Abstractions.Io.Outcomes;
using Cli.Instructions.Extensions;
using Cli.Workflow;
using Cli.Workflow.Abstractions;
using Cli.Workflow.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Cli;

public static class CliServiceCollectionExtensions
{
    public static IServiceCollection AddCli<TCliApp>(this IServiceCollection serviceCollection) where TCliApp : CliApp
    {
        serviceCollection.AddCliAbstractions();
        serviceCollection.AddCliInstructions();
        
        serviceCollection.AddCliWorkflowCommands();
        
        serviceCollection.AddSingleton<ICliWorkflow, CliWorkflow>();
        
        serviceCollection.AddSingleton<ICliCommandOutcomeIo, CliCommandOutcomeIo>();
        
        serviceCollection.AddSingleton<CliApp, TCliApp>();
        
        return serviceCollection;
    }
}