using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Attributes;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands.Personalisation.Commitments.Find;

[FactoryFor(typeof(CommitmentsCliCommand))]
public class FindCommitmentCliCommandFactory : ICliCommandFactory<FindCommitmentCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> properties)
        => instruction.SubInstructionName == CommitmentsCliCommand.SubCommandNames.Find;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> properties)
        => new FindCommitmentCliCommand();
}