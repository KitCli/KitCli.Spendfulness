using Cli.Commands.Abstractions;

namespace Cli.Spendfulness.Commands.Personalisation.Databases;

public class DatabaseCliCommand : ICliCommand
{
    public static class SubCommandNames
    {
        public const string Create = "create";
    }
}