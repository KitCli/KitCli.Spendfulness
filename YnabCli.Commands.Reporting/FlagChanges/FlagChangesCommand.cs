using Cli.Commands.Abstractions;

namespace YnabCli.Commands.Reporting.FlagChanges;

public class FlagChangesCommand : ICommand
{
    public static class ArgumentNames
    {
        public const string From = "from";
        public const string To = "to";
    }
    
    public DateOnly? From { get; set; }
    public DateOnly? To { get; set; }
}