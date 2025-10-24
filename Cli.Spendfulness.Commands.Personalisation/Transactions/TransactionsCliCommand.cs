using Cli.Commands.Abstractions;

namespace Cli.Spendfulness.Commands.Personalisation.Transactions;

public class TransactionsCliCommand : ICliCommand
{
    public static class SubCommandNames
    {
        public const string List = "list";
    }
}