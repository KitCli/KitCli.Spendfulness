using KitCli.Commands.Abstractions;
using KitCli.Commands.Abstractions.Artefacts;
using KitCli.Commands.Abstractions.Factories;
using KitCli.Instructions.Abstractions;
using KitCli.Instructions.Arguments;

namespace SpendfulnessCli.Commands.Reporting.RecurringTransactions;

public class RecurringTransactionsCliCommandFactory : ICliCommandFactory<RecurringTransactionsCliCommand>
{
    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
    {
        var fromArgument = instruction
            .Arguments
            .OfType<DateOnly>(RecurringTransactionsCliCommand.ArgumentNames.From);
        
        var toArgument = instruction
            .Arguments
            .OfType<DateOnly>(RecurringTransactionsCliCommand.ArgumentNames.To);
        
        var payeeNameArgument = instruction
            .Arguments
            .OfType<string>(RecurringTransactionsCliCommand.ArgumentNames.PayeeName);
        
        var minimumOccurrencesArgument = instruction
            .Arguments
            .OfType<int>(RecurringTransactionsCliCommand.ArgumentNames.MinimumOccurrences);

        return new RecurringTransactionsCliCommand
        {
            From = fromArgument?.ArgumentValue,
            To = toArgument?.ArgumentValue,
            PayeeName = payeeNameArgument?.ArgumentValue,
            MinimumOccurrences = minimumOccurrencesArgument?.ArgumentValue
        };
    }
}