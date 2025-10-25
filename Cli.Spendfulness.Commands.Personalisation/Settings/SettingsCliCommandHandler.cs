using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Outcomes;

namespace Cli.Spendfulness.Commands.Personalisation.Settings;

public class SettingsCliCommandHandler : ICliCommandHandler<SettingsCliCommand>
{
    public Task<CliCommandOutcome> Handle(SettingsCliCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}