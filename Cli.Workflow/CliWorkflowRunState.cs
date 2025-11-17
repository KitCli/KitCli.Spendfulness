using System.Diagnostics;
using Cli.Workflow.Abstractions;

namespace Cli.Workflow;

public class CliWorkflowRunState
{
    public readonly Stopwatch Stopwatch = new Stopwatch();
    public readonly List<CliWorkflowRunStateChange> Changes = [];

    // TODO: Find a way to STORE THINGS in state.
    public void ChangeTo(ClIWorkflowRunStateStatus stateStatusToChangeTo)
    {
        var priorState = CanChangeTo(stateStatusToChangeTo);
        
        UpdateStopwatch(stateStatusToChangeTo);

        var stateChange = new CliWorkflowRunStateChange(
            Stopwatch.Elapsed,
            priorState, 
            stateStatusToChangeTo);
        
        Changes.Add(stateChange);
    }

    private ClIWorkflowRunStateStatus CanChangeTo(ClIWorkflowRunStateStatus stateStatusToChangeTo)
    {
        var mostRecentState = Changes.LastOrDefault();
        var priorState = mostRecentState?.To ?? ClIWorkflowRunStateStatus.Created;
        
        // Can chnge from most recently changed to, to new state to change to.
        var possibleStateChange = PossibleStateChanges
            .Any(cliWorkflowRunStateChange =>
                cliWorkflowRunStateChange.IfStartedAt == priorState && 
                cliWorkflowRunStateChange.CanMoveTo == stateStatusToChangeTo);

        if (!possibleStateChange)
        {
            throw new ImpossibleStateChangeException($"Invalid state change: {priorState} > {stateStatusToChangeTo}");
        }
        
        return priorState;
    }

    private void UpdateStopwatch(ClIWorkflowRunStateStatus stateStatusToChangeTo)
    {
        if (stateStatusToChangeTo == ClIWorkflowRunStateStatus.Running)
        {
            Stopwatch.Start();
        }

        if (stateStatusToChangeTo == ClIWorkflowRunStateStatus.Finished)
        {
            Stopwatch.Stop();
        }
    }

    /// <summary>
    /// Stops a CliWorkflowRun from being re-eexecuted for another command.
    /// </summary>
    private static readonly List<PossibleCliWorkflowRunStateChange> PossibleStateChanges =
    [
        new(ClIWorkflowRunStateStatus.Created, ClIWorkflowRunStateStatus.Running),
        new(ClIWorkflowRunStateStatus.Created, ClIWorkflowRunStateStatus.InvalidAsk),
        
        new(ClIWorkflowRunStateStatus.Running, ClIWorkflowRunStateStatus.InvalidAsk),
        new(ClIWorkflowRunStateStatus.InvalidAsk, ClIWorkflowRunStateStatus.Finished),
        
        new(ClIWorkflowRunStateStatus.Running, ClIWorkflowRunStateStatus.Exceptional),
        new(ClIWorkflowRunStateStatus.Exceptional, ClIWorkflowRunStateStatus.Finished),
        
        new(ClIWorkflowRunStateStatus.Running, ClIWorkflowRunStateStatus.Finished)
    ];
}