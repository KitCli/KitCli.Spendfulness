using KitCli.Commands.Abstractions.Handlers;
using KitCli.Commands.Abstractions.Outcomes;

namespace SpendfulnessCli.Commands.Personalisation.Databases;

public class DatabaseCliCommandHandler : ICliCommandHandler<DatabaseCliCommand>
{
    public Task<CliCommandOutcome[]> Handle(DatabaseCliCommand request, CancellationToken cancellationToken)
    {
        throw new Exception("No functionality in the base command, please use a subcommand");
    }
}