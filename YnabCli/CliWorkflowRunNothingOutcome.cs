namespace YnabCli;

public class CliWorkflowRunNothingOutcome : CliWorkflowRunOutcome, ICliWorkflowRunOutcome
{
    private readonly ClIWorkflowRunState _lastCliWorkflowRunCliWorkflowRunState;
    
    public CliWorkflowRunNothingOutcome(ClIWorkflowRunState lastCliWorkflowRunState, CliIo cliIo)
        : base(CliWorkflowRunOutcomeKind.Nothing, cliIo)
    {
        _lastCliWorkflowRunCliWorkflowRunState = lastCliWorkflowRunState;
    }

    public void Do()
    {
        CliIo.Say($"Program finished in a {_lastCliWorkflowRunCliWorkflowRunState} state");
    }
}