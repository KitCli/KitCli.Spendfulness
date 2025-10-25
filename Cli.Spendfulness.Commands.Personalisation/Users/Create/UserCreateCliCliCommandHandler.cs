using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Outcomes;
using Cli.Spendfulness.Database;
using Cli.Spendfulness.Database.Users;
using Microsoft.EntityFrameworkCore;

namespace Cli.Spendfulness.Commands.Personalisation.Users.Create;

public class UserCreateCliCliCommandHandler : CliCommandHandler, ICliCommandHandler<UserCreateCliCommand>
{
    private readonly YnabCliDbContext _dbContext;

    public UserCreateCliCliCommandHandler(YnabCliDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CliCommandOutcome> Handle(UserCreateCliCommand cliCommand, CancellationToken cancellationToken)
    {
        var activeUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Active, cancellationToken);
        
        var user = new User
        {
            Name = cliCommand.UserName,
            Active = activeUser == null 
        };
        
        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Compile($"Created User \"{cliCommand.UserName}\".");
    }
}