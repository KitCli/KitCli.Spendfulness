using KitCli.Commands.Abstractions;
using KitCli.Commands.Abstractions.Artefacts;
using KitCli.Commands.Abstractions.Factories;
using KitCli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands.Reporting.AverageYearlySpending;

public class AverageYearlySpendingCliCommandFactory : ICliCommandFactory<AverageYearlySpendingCliCommand>
{
    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => new AverageYearlySpendingCliCommand();
}