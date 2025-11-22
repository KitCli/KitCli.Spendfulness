using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands.Reporting.AverageYearlyPay;

public class AverageYearlyPayCliCommandFactory : ICliCommandFactory<AverageYearlyPayCliCommand>
{
    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => new AverageYearlyPayCliCommand();
}