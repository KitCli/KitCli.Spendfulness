using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Outcomes;

namespace YnabCli.Commands.Personalisation.Settings;

public class SettingsCommandHandler : ICommandHandler<SettingsCommand>
{
    public Task<CliCommandOutcome> Handle(SettingsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}