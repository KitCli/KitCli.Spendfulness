using Cli.Commands.Abstractions;

namespace YnabCli.Commands.Personalisation.Transactions;

public class TransactionsCommand : ICommand
{
    public static class SubCommandNames
    {
        public const string List = "list";
    }
}