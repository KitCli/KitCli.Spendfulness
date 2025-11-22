using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands.Reusable.Table;

public class TableCliCommandGenerator : ICliCommandFactory<TableCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> artefacts) => artefacts.Count == 0;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts) => new TableCliCommand();
}