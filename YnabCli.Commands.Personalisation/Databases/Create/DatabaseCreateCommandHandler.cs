using ConsoleTables;
using YnabCli.Abstractions;
using YnabCli.Commands.Handlers;
using YnabCli.Database;

namespace YnabCli.Commands.Personalisation.Databases.Create;

public class DatabaseCreateCommandHandler : CommandHandler, ICommandHandler<DatabaseCreateCommand>
{
    private readonly YnabCliDbContext _dbContext;

    public DatabaseCreateCommandHandler(YnabCliDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CliCommandOutcome> Handle(DatabaseCreateCommand request, CancellationToken cancellationToken)
    {
        await _dbContext.Database.EnsureCreatedAsync(cancellationToken);

        return Compile("Database exists");
    }
}