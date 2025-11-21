using Cli.Commands.Abstractions.Extensions;
using Cli.Workflow.Commands.Exit;
using Microsoft.Extensions.DependencyInjection;

namespace Cli.Workflow.Commands;

public static class WorkflowCommandsServiceCollectionExtensions
{
    public static IServiceCollection AddCliWorkflowCommands(this IServiceCollection services)
        => services
            .AddCommandsFromAssembly(typeof(ExitCliCommand).Assembly)
            .AddSingleton<ICliWorkflowCommandProvider, CliWorkflowCommandProvider>();
}