using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Attributes;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;

namespace SpendfulnessCli.Commands.Personalisation.Settings.Create;

[FactoryFor(typeof(SettingCliCommand))]
public class CreateSettingCliCommandFactory : ICliCommandFactory<CreateSettingCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> properties)
        => instruction.SubInstructionName == SettingCliCommand.SubCommandNames.Create;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> properties)
    {
        var nameArgument = instruction.Arguments.OfRequiredType<string>(CreateSettingCliCommand.ArgumentNames.Type);
        var valueArgument = instruction.Arguments.OfRequiredType<string>(CreateSettingCliCommand.ArgumentNames.Value);

        return new CreateSettingCliCommand
        {
            Type = nameArgument.ArgumentValue,
            Value = valueArgument.ArgumentValue,
        };
    }
}