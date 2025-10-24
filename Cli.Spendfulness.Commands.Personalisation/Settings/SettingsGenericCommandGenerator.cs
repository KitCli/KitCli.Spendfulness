using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;
using Cli.Spendfulness.Commands.Personalisation.Settings.Create;
using Cli.Spendfulness.Commands.Personalisation.Settings.View;

namespace Cli.Spendfulness.Commands.Personalisation.Settings;

public class SettingsGenericCommandGenerator : ICommandGenerator<SettingsCliCommand>
{
    public ICliCommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        return subCommandName switch
        {
            SettingsCliCommand.SubCommandNames.Create => GenerateCreateCommand(arguments),
            SettingsCliCommand.SubCommandNames.View => new SettingsViewCliCommand(),
            _ => new SettingsCliCommand()
        };
    }

    private SettingCreateCliCommand GenerateCreateCommand(List<ConsoleInstructionArgument> arguments)
    {
        var nameArgument = arguments.OfRequiredType<string>(SettingCreateCliCommand.ArgumentNames.Type);
        var valueArgument = arguments.OfRequiredType<string>(SettingCreateCliCommand.ArgumentNames.Value);

        return new SettingCreateCliCommand
        {
            Type = nameArgument.ArgumentValue,
            Value = valueArgument.ArgumentValue,
        };
    }
}