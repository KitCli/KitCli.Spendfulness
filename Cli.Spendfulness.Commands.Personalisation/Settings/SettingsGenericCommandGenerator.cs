using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;
using Cli.Spendfulness.Commands.Personalisation.Settings.Create;
using Cli.Spendfulness.Commands.Personalisation.Settings.View;

namespace Cli.Spendfulness.Commands.Personalisation.Settings;

public class SettingsGenericCommandGenerator : ICommandGenerator<SettingsCliCommand>
{
    public ICliCommand Generate(CliInstruction instruction)
        => instruction.SubInstructionName switch
        {
            SettingsCliCommand.SubCommandNames.Create => GenerateCreateCommand(instruction.Arguments),
            SettingsCliCommand.SubCommandNames.View => new SettingsViewCliCommand(),
            _ => new SettingsCliCommand()
        };
    
    private SettingCreateCliCommand GenerateCreateCommand(List<CliInstructionArgument> arguments)
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