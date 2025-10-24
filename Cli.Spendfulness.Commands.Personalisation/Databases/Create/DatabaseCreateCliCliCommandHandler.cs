using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Outcomes;
using Cli.Spendfulness.Database;

namespace Cli.Spendfulness.Commands.Personalisation.Databases.Create;

public class DatabaseCreateCliCliCommandHandler : CliCommandHandler, ICliCommandHandler<DatabaseCreateCliCommand>
{
    private readonly YnabCliDbContext _dbContext;

    public DatabaseCreateCliCliCommandHandler(YnabCliDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CliCommandOutcome> Handle(DatabaseCreateCliCommand request, CancellationToken cancellationToken)
    {
        await _dbContext.Database.EnsureCreatedAsync(cancellationToken);

        return Compile("Database exists");
    }
}