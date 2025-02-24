namespace YnabCli.Commands.User;

public class UserCommand : ICommand
{
    public const string CommandName = "user";
    public const string ShorthandCommandName = "u";

    public static class SubCommandNames
    {
        public const string Create = "create";
    }
}