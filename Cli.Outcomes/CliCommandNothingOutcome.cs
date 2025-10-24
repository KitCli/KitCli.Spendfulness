using Cli.Workflow.Abstractions;

namespace Cli.Outcomes;

public class CliCommandNothingOutcome(ClIWorkflowRunState lastCliWorkflowRunState)
    : CliCommandOutcome(CliWorkflowRunOutcomeKind.Nothing)
{
    public ClIWorkflowRunState LastCliWorkflowRunState = lastCliWorkflowRunState;
}