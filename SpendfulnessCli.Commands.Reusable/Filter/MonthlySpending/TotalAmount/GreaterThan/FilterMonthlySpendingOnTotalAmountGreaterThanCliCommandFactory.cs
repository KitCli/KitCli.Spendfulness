using KitCli.Commands.Abstractions;
using KitCli.Commands.Abstractions.Artefacts;
using KitCli.Commands.Abstractions.Factories;
using KitCli.Instructions.Abstractions;
using KitCli.Instructions.Arguments;
using Spendfulness.Aggregation.Aggregates;

namespace SpendfulnessCli.Commands.Reusable.Filter.MonthlySpending.TotalAmount.GreaterThan;

public class FilterMonthlySpendingOnTotalAmountGreaterThanCliCommandFactory
    : FilterMonthlySpendingOnCliCommandFactory, ICliCommandFactory<FilterCliCommand>
{
    public override bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> artefacts)
    {
        var previouslyCalledMonthlySpendingCommandAndFilterArgumentPresent = base.CanCreateWhen(instruction, artefacts);

        var greaterThanArgument = instruction
            .Arguments
            .OfType<decimal>(FilterMonthlySpendingOnTotalAmountGreaterThanCliCommand.ArgumentNames.GreaterThan);

        return previouslyCalledMonthlySpendingCommandAndFilterArgumentPresent && greaterThanArgument != null;
    }
    
    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
    {
        var aggregatorArtefact = artefacts
            .OfListAggregatorType<TransactionMonthTotalAggregate>();
        
        var filterOnArgument = instruction
            .Arguments
            .OfRequiredType<string>(FilterCliCommand.ArgumentNames.FilterOn);

        var greaterThanArgument = instruction
            .Arguments
            .OfRequiredType<decimal>(FilterMonthlySpendingOnTotalAmountGreaterThanCliCommand.ArgumentNames.GreaterThan);

        return new FilterMonthlySpendingOnTotalAmountGreaterThanCliCommand(
            aggregatorArtefact!.ArtefactValue,
            filterOnArgument.ArgumentValue,
            greaterThanArgument.ArgumentValue);
    }
}