using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands.Personalisation.Databases;

public class DatabaseCliCommandFactory : ICliCommandFactory<DatabaseCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => instruction.SubInstructionName is null;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => new DatabaseCliCommand();
}