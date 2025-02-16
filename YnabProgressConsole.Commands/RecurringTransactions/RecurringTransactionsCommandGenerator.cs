using YnabProgressConsole.Instructions.InstructionArguments;

namespace YnabProgressConsole.Commands.RecurringTransactions;

public class RecurringTransactionsCommandGenerator : ICommandGenerator, ITypedCommandGenerator<RecurringTransactionsCommand>
{
    public ICommand Generate(List<InstructionArgument> arguments)
    {
        var fromArgument = arguments
            .OfType<DateOnly>(RecurringTransactionsCommand.ArgumentNames.From);
        
        var toArgument = arguments
            .OfType<DateOnly>(RecurringTransactionsCommand.ArgumentNames.To);
        
        var payeeNameArgument = arguments
            .OfType<string>(RecurringTransactionsCommand.ArgumentNames.PayeeName);

        return new RecurringTransactionsCommand
        {
            From = fromArgument?.ArgumentValue,
            To = toArgument?.ArgumentValue,
            PayeeName = payeeNameArgument?.ArgumentValue
        };
    }
}