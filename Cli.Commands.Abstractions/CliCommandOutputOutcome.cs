using Cli.Outcomes;

namespace Cli.Commands.Abstractions;

public class CliCommandOutputOutcome(string output) 
    : CliCommandOutcome(CliWorkflowRunOutcomeKind.Output)
{
    public string Output = output;
}