using ConsoleTables;

namespace YnabCli.Commands.Database;

public class DatabaseCommandHandler : ICommandHandler<DatabaseCommand>
{
    public Task<ConsoleTable> Handle(DatabaseCommand request, CancellationToken cancellationToken)
    {
        throw new Exception("No functionality in the base command, please use a subcommand");
    }
}