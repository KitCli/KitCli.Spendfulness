using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands.Personalisation.Users.Active;

public class ActiveUserCliCommandFactory : ICliCommandFactory<UserCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => instruction.SubInstructionName == UserCliCommand.SubCommandNames.Active;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => new ActiveUserCliCommand();
}