namespace YnabProgressConsole.Commands.RecurringTransactions;

public class RecurringTransactionsCommandGenerator : ICommandGenerator
{
    public ICommand Generate(List<string> arguments)
    {
        return new RecurringTransactionsCommand();
    }
}