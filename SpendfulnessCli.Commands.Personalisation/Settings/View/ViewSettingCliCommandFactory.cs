using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Attributes;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;
using SpendfulnessCli.Commands.Personalisation.Settings.View;

namespace SpendfulnessCli.Commands.Personalisation.Settings;

[FactoryFor(typeof(SettingCliCommand))]
public class ViewSettingCliCommandFactory : ICliCommandFactory<ViewSettingCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> properties)
        => instruction.SubInstructionName == SettingCliCommand.SubCommandNames.View;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> properties)
        => new ViewSettingCliCommand();
}