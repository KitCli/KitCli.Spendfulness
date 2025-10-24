using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Outcomes;
using ConsoleTables;
using YnabCli.Commands.Handlers;

namespace YnabCli.Commands.Personalisation.Databases;

public class DatabaseCommandHandler : ICommandHandler<DatabaseCommand>
{
    public Task<CliCommandOutcome> Handle(DatabaseCommand request, CancellationToken cancellationToken)
    {
        throw new Exception("No functionality in the base command, please use a subcommand");
    }
}