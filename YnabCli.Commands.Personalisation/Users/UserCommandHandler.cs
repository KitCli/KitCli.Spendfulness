using ConsoleTables;
using YnabCli.Abstractions;
using YnabCli.Commands.Handlers;

namespace YnabCli.Commands.Personalisation.Users;

public class UserCommandHandler : ICommandHandler<UserCommand>
{
    public Task<CliCommandOutcome> Handle(UserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}