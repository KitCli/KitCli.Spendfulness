using Cli.Workflow.Abstractions;

namespace Cli.Workflow.Tests;

public abstract class CliWorkflowRunStateTests
{
    protected static CliWorkflowRunState GetPreparedState(IEnumerable<ClIWorkflowRunStateStatus> priorStates)
    {
        var state = new CliWorkflowRunState();
        
        foreach (var priorState in priorStates)
        {
            state.ChangeTo(priorState);
        }

        return state;
    }
}