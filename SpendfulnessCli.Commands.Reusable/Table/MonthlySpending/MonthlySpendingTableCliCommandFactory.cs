using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Factories;
using Cli.Commands.Abstractions.Outcomes.Reusable;
using Cli.Instructions.Abstractions;
using Cli.Workflow.Commands.MissingOutcomes;
using SpendfulnessCli.Aggregation.Aggregates;
using SpendfulnessCli.Commands.Reporting.MonthlySpending;

namespace SpendfulnessCli.Commands.Reusable.Table.MonthlySpending;

public class MonthlySpendingTableCliCommandFactory : ICliCommandFactory<TableCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => artefacts.LastCommandRanWas<MonthlySpendingCliCommand>();
    
    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
    {
        var monthlySpendingAggregator = artefacts.GetListAggregator<TransactionMonthTotalAggregate>();
        if (monthlySpendingAggregator == null)
        {
            return new MissingOutcomesCliCommand([
                nameof(CliCommandListAggregatorOutcome<TransactionMonthTotalAggregate>)
            ]);
        }
        
        return new MonthlySpendingTableCliCommand(monthlySpendingAggregator);
    }
}