namespace YnabCli.Commands.Personalisation.Accounts.Create;

public record AccountsCreateCommand(string Name, string Type) : ICommand
{
    public static class ArgumentNames
    {
        public const string Name = "name";
        public const string Type = "type";
    }
}