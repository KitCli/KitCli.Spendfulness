using KitCli.Commands.Abstractions.Handlers;
using KitCli.Commands.Abstractions.Outcomes;
using Spendfulness.Database;
using Spendfulness.Database.Sqlite;

namespace SpendfulnessCli.Commands.Personalisation.Databases.Create;

public class CreateDatabaseCliCommandHandler(SpendfulnessDbContext dbContext)
    : CliCommandHandler, ICliCommandHandler<CreateDatabaseCliCommand>
{
    public async Task<CliCommandOutcome[]> Handle(CreateDatabaseCliCommand request, CancellationToken cancellationToken)
    {
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);

        return OutcomeAs("Database exists");
    }
}