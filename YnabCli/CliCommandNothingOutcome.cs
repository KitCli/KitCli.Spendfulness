using YnabCli.Abstractions;

namespace YnabCli;

public class CliCommandNothingOutcome(ClIWorkflowRunState lastCliWorkflowRunState)
    : CliCommandOutcome(CliWorkflowRunOutcomeKind.Nothing)
{
    public ClIWorkflowRunState LastCliWorkflowRunState = lastCliWorkflowRunState;
}