namespace YnabProgressConsole.Commands.FlagChanges;

public class FlagChangesCommand : ICommand
{
    public const string CommandName = "flag-changes";
    public const string ShorthandCommandName = "fc";
    
    public static class ArgumentNames
    {
        public const string From = "from";
    }
    
    public DateOnly? From { get; set; }
}