using Cli;
using Cli.Commands.Abstractions.Io.Outcomes;
using Cli.Workflow.Abstractions;

public class SplitterCli : CliApp
{
    public SplitterCli(ICliWorkflow workflow, ICliCommandOutcomeIo io) : base(workflow, io)
    {
    }
}