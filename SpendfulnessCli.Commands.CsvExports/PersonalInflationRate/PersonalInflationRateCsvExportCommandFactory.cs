using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands.CsvExports.PersonalInflationRate;

public class PersonalInflationRateCsvExportCommandFactory : ICliCommandFactory<PersonalInflationRateCsvExportCommand>
{
    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
    {
        throw new NotImplementedException();
    }
}