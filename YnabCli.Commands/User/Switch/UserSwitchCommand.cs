namespace YnabCli.Commands.User.Switch;

public class UserSwitchCommand : ICommand
{
    public static class ArugmentNames
    {
        public const string UserName = "user-name";
    }
    
    public string? UserName { get; set; }
}