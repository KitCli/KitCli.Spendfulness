using Cli.Commands.Abstractions;

namespace Cli.Spendfulness.Commands.Personalisation.Accounts.Identify;

public record AccountsIdentifyCliCommand(string YnabAccountName, string CustomAccountTypeName) : ICliCommand
{
    public static class ArgumentNames
    {
        public const string Name = "name";
        public const string Type = "type";
    }
}