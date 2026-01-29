using KitCli.Commands.Abstractions;
using KitCli.Commands.Abstractions.Artefacts;
using KitCli.Commands.Abstractions.Factories;
using KitCli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands.Personalisation.Settings.View;

public class ViewSettingCliCommandFactory : ICliCommandFactory<SettingCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => instruction.SubInstructionName == SettingCliCommand.SubCommandNames.View;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => new ViewSettingCliCommand();
}