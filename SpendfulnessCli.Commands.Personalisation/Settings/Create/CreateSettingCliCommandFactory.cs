using KitCli.Commands.Abstractions;
using KitCli.Commands.Abstractions.Artefacts;
using KitCli.Commands.Abstractions.Factories;
using KitCli.Instructions.Abstractions;
using KitCli.Instructions.Arguments;

namespace SpendfulnessCli.Commands.Personalisation.Settings.Create;

public class CreateSettingCliCommandFactory : ICliCommandFactory<SettingCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => instruction.SubInstructionName == SettingCliCommand.SubCommandNames.Create;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
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