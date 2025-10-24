using Cli.Commands.Abstractions;

namespace YnabCli.Commands.Personalisation.Users;

public class UserCommand : ICommand
{
    public static class SubCommandNames
    {
        public const string Create = "create";
        public const string Switch = "switch";
        public const string Active = "active";
    }
}