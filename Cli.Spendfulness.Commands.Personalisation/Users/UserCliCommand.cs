using Cli.Commands.Abstractions;

namespace Cli.Spendfulness.Commands.Personalisation.Users;

public class UserCliCommand : ICliCommand
{
    public static class SubCommandNames
    {
        public const string Create = "create";
        public const string Switch = "switch";
        public const string Active = "active";
    }
}