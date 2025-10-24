using Cli.Commands.Abstractions;

namespace YnabCli.Commands.Personalisation.Databases;

public class DatabaseCommand : ICommand
{
    public static class SubCommandNames
    {
        public const string Create = "create";
    }
}