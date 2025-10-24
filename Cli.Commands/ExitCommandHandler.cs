using Cli.Commands.Abstractions;

namespace Cli.Commands;

public class ExitCommandHandler : CommandHandler, ICommandHandler<ExitCommand>
{
    private readonly CliWorkflow _cliWorkflow;

    public ExitCommandHandler(CliWorkflow cliWorkflow)
    {
        _cliWorkflow = cliWorkflow;
    }

    public Task<CliCommandOutcome> Handle(ExitCommand request, CancellationToken cancellationToken)
    {
        _cliWorkflow.Stop();
        
        var nothing = new CliCommandNothingOutcome();
        return Task.FromResult<CliCommandOutcome>(nothing);
    }
}