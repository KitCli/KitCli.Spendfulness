using ConsoleTables;

namespace YnabCli.Commands.Setting;

public class SettingsCommandHandler : ICommandHandler<SettingsCommand>
{
    public Task<ConsoleTable> Handle(SettingsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}