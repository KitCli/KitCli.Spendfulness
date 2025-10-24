namespace Cli.Outcomes;

public class CliCommandOutputOutcome(string output) 
    : CliCommandOutcome(CliWorkflowRunOutcomeKind.Output)
{
    public string Output = output;
}