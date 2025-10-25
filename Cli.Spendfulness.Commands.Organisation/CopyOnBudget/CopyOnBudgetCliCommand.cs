using Cli.Commands.Abstractions;

namespace Cli.Spendfulness.Commands.Organisation.CopyOnBudget;

public class CopyOnBudgetCliCommand(Guid accountId) : ICliCommand
{
    public static class ArgumentNames
    {
        public const string AccountId = "account-id";
    }

    public Guid AccountId { get; init; } = accountId;
}