using KitCli.Commands.Abstractions;
using KitCli.Commands.Abstractions.Artefacts;
using KitCli.Commands.Abstractions.Factories;
using KitCli.Commands.Abstractions.Outcomes.Reusable;
using KitCli.Instructions.Abstractions;
using KitCli.Workflow.Commands.MissingOutcomes;
using Spendfulness.Aggregation.Aggregates;
using SpendfulnessCli.Commands.Reusable.MonthlySpending;

namespace SpendfulnessCli.Commands.Reusable.Table.MonthlySpending;

public class MonthlySpendingTableCliCommandFactory
    : MonthlySpendingCliCommandFactory, ICliCommandFactory<TableCliCommand>
{
    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
    {
        var aggregatorArtefact = artefacts.OfListAggregatorType<TransactionMonthTotalAggregate>();
        
        if (aggregatorArtefact == null)
        {
            return new MissingOutcomesCliCommand([
                nameof(ListAggregatorCliCommandOutcome<TransactionMonthTotalAggregate>)
            ]);
        }

        return new MonthlySpendingTableCliCommand(aggregatorArtefact.ArtefactValue);
    }
}