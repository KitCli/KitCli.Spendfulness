using Cli.Commands.Abstractions.Io;
using Cli.Commands.Abstractions.Outcomes;
using Cli.Workflow;

namespace Cli.Spendfulness;

public class SpendfulnessCli : OriginalCli
{
    public SpendfulnessCli(CliWorkflow workflow, CliCommandOutcomeIo io)
        : base(workflow, io)
    {
    }

    protected override void OnRun(CliWorkflow workflow, CliIo io)
    {
        io.Say($"New world CLI started");
    }

    protected override void OnRunCreated(CliWorkflowRun workflowRun, CliIo io)
    {
        io.Say($"New world CLI run created");
    }

    protected override void OnRunStarted(CliWorkflowRun workflowRun, CliIo io)
    {
        io.Say($"New world CLI run started");
    }
}