using Cli.Commands.Abstractions;

namespace SpendfulnessCli.Commands.Chat.Chat;

public record ChatCliCommand(string Prompt) : CliCommand
{
    // TODO: Could I inject these through settings? commandsettings.json?
    public static class SubCommandNames
    {
        public const string PreloadDatabase = "preload-database";
        public const string ClearDatabase = "clear-database";
    }
    
    public static class ArgumentNames
    {
        public const string Prompt = "prompt";
    }
}