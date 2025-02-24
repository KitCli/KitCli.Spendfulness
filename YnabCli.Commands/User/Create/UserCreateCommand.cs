namespace YnabCli.Commands.User.Create;

public class UserCreateCommand : ICommand
{
    public static class ArugmentNames
    {
        public const string UserName = "user-name";
    }
    
    public string? UserName { get; set; }
}