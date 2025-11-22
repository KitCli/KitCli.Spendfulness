using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Attributes;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;
using SpendfulnessCli.Commands.Personalisation.Databases.Create;

namespace SpendfulnessCli.Commands.Personalisation.Databases;

[FactoryFor(typeof(DatabaseCliCommand))]
public class CreateDatabaseCliCommandFactory : ICliCommandFactory<CreateDatabaseCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> properties)
        => instruction.SubInstructionName == DatabaseCliCommand.SubCommandNames.Create;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> properties)
        => new CreateDatabaseCliCommand();
}