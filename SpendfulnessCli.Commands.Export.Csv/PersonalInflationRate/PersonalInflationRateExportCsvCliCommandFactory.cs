using KitCli.Commands.Abstractions;
using KitCli.Commands.Abstractions.Artefacts;
using KitCli.Commands.Abstractions.Factories;
using KitCli.Instructions.Abstractions;
using KitCli.Instructions.Arguments;

namespace SpendfulnessCli.Commands.Export.Csv.PersonalInflationRate;

public class PersonalInflationRateExportCsvCliCommandFactory : ICliCommandFactory<PersonalInflationRateExportCsvCliCommand>
{
    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
    {
        var outputFileSystemInfoArgument = instruction
            .Arguments
            .OfRequiredType<DirectoryInfo>(PersonalInflationRateExportCsvCliCommand.ArgumentNames.OutputFileSystemPath);
        
        return new PersonalInflationRateExportCsvCliCommand(outputFileSystemInfoArgument.ArgumentValue);
    }
}