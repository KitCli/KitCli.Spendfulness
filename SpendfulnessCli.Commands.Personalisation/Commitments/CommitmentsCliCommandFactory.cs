using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Attributes;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands.Personalisation.Commitments;

[FactoryFor(typeof(CommitmentsCliCommand))]
public class CommitmentsCliCommandFactory : ICliCommandFactory<CommitmentsCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> properties)
        => instruction.SubInstructionName is null;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> properties)
        => new CommitmentsCliCommand();
}