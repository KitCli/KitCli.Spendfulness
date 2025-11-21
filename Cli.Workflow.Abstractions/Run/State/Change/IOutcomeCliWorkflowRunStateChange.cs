using Cli.Commands.Abstractions.Outcomes;

namespace Cli.Workflow.Abstractions.Run.State.Change;

public interface IOutcomeCliWorkflowRunStateChange : ICliWorkflowRunStateChange
{
    CliCommandOutcome[] Outcomes { get; }
}