using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;

namespace Cli.Ynab.Commands.Reporting.RecurringTransactions;

public class RecurringTransactionsGenericCommandGenerator : ICommandGenerator<RecurringTransactionsCliCommand>
{
    public ICliCommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        var fromArgument = arguments
            .OfType<DateOnly>(RecurringTransactionsCliCommand.ArgumentNames.From);
        
        var toArgument = arguments
            .OfType<DateOnly>(RecurringTransactionsCliCommand.ArgumentNames.To);
        
        var payeeNameArgument = arguments
            .OfType<string>(RecurringTransactionsCliCommand.ArgumentNames.PayeeName);
        
        var minimumOccurrencesArgument = arguments
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