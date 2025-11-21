using System.Diagnostics;
using Cli.Commands.Abstractions.Outcomes;
using Cli.Instructions.Abstractions;
using Cli.Workflow.Abstractions.Run.State.Change;

namespace Cli.Workflow.Abstractions;

public interface ICliWorkflowRunState
{
    Stopwatch Stopwatch { get; }

    List<ICliWorkflowRunStateChange> Changes { get; }

    bool WasChangedTo(ClIWorkflowRunStateStatus status);

    List<IOutcomeCliWorkflowRunStateChange> AllOutcomeStateChanges();

    void ChangeTo(ClIWorkflowRunStateStatus statusToChangeTo);

    void ChangeTo(ClIWorkflowRunStateStatus statusToChangeTo, CliInstruction instruction);

    void ChangeTo(ClIWorkflowRunStateStatus statusToChangeTo, CliCommandOutcome[] outcomes);
}