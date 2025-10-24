using ConsoleTables;
using YnabCli.Abstractions;
using YnabCli.Commands.Handlers;

namespace YnabCli.Commands.Personalisation.Settings;

public class SettingsCommandHandler : ICommandHandler<SettingsCommand>
{
    public Task<CliCommandOutcome> Handle(SettingsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}