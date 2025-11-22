using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands.Personalisation.Settings.View;

public class ViewSettingCliCommandFactory : ICliCommandFactory<SettingCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => instruction.SubInstructionName == SettingCliCommand.SubCommandNames.View;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => new ViewSettingCliCommand();
}