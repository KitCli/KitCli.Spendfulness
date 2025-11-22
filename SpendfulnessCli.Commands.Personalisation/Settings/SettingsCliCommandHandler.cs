using Cli.Commands.Abstractions.Handlers;
using Cli.Commands.Abstractions.Outcomes;

namespace SpendfulnessCli.Commands.Personalisation.Settings;

public class SettingsCliCommandHandler : ICliCommandHandler<SettingCliCommand>
{
    public Task<CliCommandOutcome[]> Handle(SettingCliCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}