using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands.Personalisation.Commitments.Find;

public class FindCommitmentCliCommandFactory : ICliCommandFactory<CommitmentsCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => instruction.SubInstructionName == CommitmentsCliCommand.SubCommandNames.Find;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => new FindCommitmentCliCommand();
}