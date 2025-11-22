using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Attributes;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;
using SpendfulnessCli.Commands.Personalisation.Users.Active;

namespace SpendfulnessCli.Commands.Personalisation.Users;

[FactoryFor(typeof(UserCliCommand))]
public class ActiveUserCliCommandFactory : ICliCommandFactory<ActiveUserCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> properties)
        => instruction.SubInstructionName == UserCliCommand.SubCommandNames.Active;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> properties)
        => new ActiveUserCliCommand();
}