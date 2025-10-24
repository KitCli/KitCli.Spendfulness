using ConsoleTables;
using Microsoft.EntityFrameworkCore;
using YnabCli.Abstractions;
using YnabCli.Commands.Handlers;
using YnabCli.Database;

namespace YnabCli.Commands.Personalisation.Users.Active;

public class UserActiveCommandHandler : CommandHandler, ICommandHandler<UserActiveCommand>
{
    private readonly YnabCliDbContext _dbContext;

    public UserActiveCommandHandler(YnabCliDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CliCommandOutcome> Handle(UserActiveCommand request, CancellationToken cancellationToken)
    {
        var activeUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Active);
        
        return activeUser != null
            ? Compile($"Active user is {activeUser.Name}")
            : Compile($"No active user.");
    }
}