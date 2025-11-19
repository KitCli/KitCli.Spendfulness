using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Outcomes;

namespace SpendfulnessCli.Commands.Reporting.Table;

public class TableCliCommandHandler : ICliCommandHandler<TableCliCommand>
{
    public Task<CliCommandOutcome> Handle(TableCliCommand request, CancellationToken cancellationToken)
    {
        var outcome =  new CliCommandNothingOutcome();
        return Task.FromResult<CliCommandOutcome>(outcome);
    }
}