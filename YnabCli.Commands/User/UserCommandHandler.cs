using ConsoleTables;

namespace YnabCli.Commands.User;

public class UserCommandHandler : ICommandHandler<UserCommand>
{
    public Task<ConsoleTable> Handle(UserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}