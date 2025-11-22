using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Attributes;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;
using SpendfulnessCli.Commands.Personalisation.Settings;

namespace SpendfulnessCli.Commands.Personalisation.Users;

[FactoryFor(typeof(SettingCliCommand))]
public class UserCliCommandFactory : ICliCommandFactory<UserCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> properties)
        => instruction.SubInstructionName is null;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> properties)
        => new UserCliCommand();
}