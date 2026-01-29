using KitCli.Commands.Abstractions;
using KitCli.Commands.Abstractions.Artefacts;
using KitCli.Commands.Abstractions.Factories;
using KitCli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands.Personalisation.Commitments.Find;

public class FindCommitmentCliCommandFactory : ICliCommandFactory<CommitmentsCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => instruction.SubInstructionName == CommitmentsCliCommand.SubCommandNames.Find;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => new FindCommitmentCliCommand();
}