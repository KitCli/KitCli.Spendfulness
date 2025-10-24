using Cli.Commands.Abstractions;

namespace Cli.Spendfulness.Commands.Personalisation.Settings;

public class SettingsCliCommand : ICliCommand
{
    public static class SubCommandNames
    {
        public const string Create = "create";
        public const string View = "view";
    }
}