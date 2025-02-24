using YnabCli.Commands.Setting.Create;
using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands.Setting;

public class SettingsCommandGenerator : ICommandGenerator, ITypedCommandGenerator<SettingsCommand>
{
    public ICommand Generate(string? subCommandName, List<InstructionArgument> arguments)
    {
        if (subCommandName == SettingsCommand.SubCommandNames.Create)
        {
            return GenerateCreateCommand(arguments);
        }

        return new SettingsCommand();
    }

    private SettingCreateCommand GenerateCreateCommand(List<InstructionArgument> arguments)
    {
        var nameArgument = arguments.OfType<string>(SettingCreateCommand.ArgumentNames.Type);
        var valueArgument = arguments.OfType<string>(SettingCreateCommand.ArgumentNames.Value);

        return new SettingCreateCommand
        {
            Type = nameArgument?.ArgumentValue,
            Value = valueArgument?.ArgumentValue,
        };
    }
}