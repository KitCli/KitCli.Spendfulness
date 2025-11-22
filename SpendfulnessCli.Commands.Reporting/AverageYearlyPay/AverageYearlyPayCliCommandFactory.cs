using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Attributes;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands.Reporting.AverageYearlyPay;

[FactoryFor(typeof(AverageYearlyPayCliCommandFactory))]
public class AverageYearlyPayCliCommandFactory : ICliCommandFactory<AverageYearlyPayCliCommand>
{
    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> properties)
        => new AverageYearlyPayCliCommand();
}