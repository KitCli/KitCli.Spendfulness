using Cli.Workflow.Abstractions;

namespace Cli.Workflow;

public class CliWorkflowRunState
{
    private readonly List<CliWorkflowRunStateChange> _stateChanges = [];

    public void ChangeTo(ClIWorkflowRunState stateToChangeTo)
    {
        var mostRecentState = _stateChanges.LastOrDefault();
        var currentState = mostRecentState?.MovedTo ?? ClIWorkflowRunState.NotInitialized;
        
        // Can chnge from most recently changed to, to new state to change to.
        var possibleStateChange = PossibleStateChanges
            .Any(cliWorkflowRunStateChange =>
                cliWorkflowRunStateChange.StartedAt == currentState && 
                cliWorkflowRunStateChange.MovedTo == stateToChangeTo);

        if (!possibleStateChange)
        {
            throw new ImpossibleStateChangeException($"Invalid state change: {currentState} > {stateToChangeTo}");
        }

        var newState = new CliWorkflowRunStateChange(currentState, stateToChangeTo);
        _stateChanges.Add(newState);
    }

    // TODO: CLI - Does this matter at all?
    private static readonly List<CliWorkflowRunStateChange> PossibleStateChanges =
    [
        new(ClIWorkflowRunState.NotInitialized, ClIWorkflowRunState.Created),
        new(ClIWorkflowRunState.Created, ClIWorkflowRunState.Running),
        
        new(ClIWorkflowRunState.Running, ClIWorkflowRunState.InvalidAsk),
        new(ClIWorkflowRunState.InvalidAsk, ClIWorkflowRunState.Finished),
        
        new(ClIWorkflowRunState.Running, ClIWorkflowRunState.Exceptional),
        new(ClIWorkflowRunState.Exceptional, ClIWorkflowRunState.Finished),
        
        new(ClIWorkflowRunState.Running, ClIWorkflowRunState.Finished)
    ];
}