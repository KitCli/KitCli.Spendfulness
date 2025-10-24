using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Outcomes;
using Cli.Workflow;

namespace Cli.Commands;

public class ExitCliCliCommandHandler : CliCommandHandler, ICliCommandHandler<ExitCliCommand>
{
    private readonly CliWorkflow _cliWorkflow;

    public ExitCliCliCommandHandler(CliWorkflow cliWorkflow)
    {
        _cliWorkflow = cliWorkflow;
    }

    public Task<CliCommandOutcome> Handle(ExitCliCommand request, CancellationToken cancellationToken)
    {
        _cliWorkflow.Stop();
        
        var nothing = new CliCommandNothingOutcome();
        return Task.FromResult<CliCommandOutcome>(nothing);
    }
}