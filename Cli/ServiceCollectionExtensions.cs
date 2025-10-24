using Cli.Workflow;
using Microsoft.Extensions.DependencyInjection;

namespace Cli;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCli<TCliApp>(this IServiceCollection serviceCollection) where TCliApp : CliApp
    {
        serviceCollection.AddSingleton<CliWorkflowCommandProvider>();
        serviceCollection.AddSingleton<CliWorkflow>();
        serviceCollection.AddSingleton<TCliApp>();
        
        return serviceCollection;
    }
}