using KitCli.Commands.Abstractions.Handlers;
using KitCli.Commands.Abstractions.Outcomes;

namespace SpendfulnessCli.Commands.Personalisation.Settings;

public class SettingsCliCommandHandler : ICliCommandHandler<SettingCliCommand>
{
    public Task<CliCommandOutcome[]> Handle(SettingCliCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}