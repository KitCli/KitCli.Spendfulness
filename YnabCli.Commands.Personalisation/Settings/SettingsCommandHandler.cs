using ConsoleTables;
using YnabCli.Commands.Handlers;

namespace YnabCli.Commands.Personalisation.Settings;

public class SettingsCommandHandler : ICommandHandler<SettingsCommand>
{
    public Task<ConsoleTable> Handle(SettingsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}