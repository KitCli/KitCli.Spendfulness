using KitCli.Commands.Abstractions.Handlers;
using KitCli.Commands.Abstractions.Outcomes;

namespace SpendfulnessCli.Commands.Personalisation.Users;

public class UserCliCommandHandler : ICliCommandHandler<UserCliCommand>
{
    public Task<CliCommandOutcome[]> Handle(UserCliCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}