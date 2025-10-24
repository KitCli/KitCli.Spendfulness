using Cli.Commands.Abstractions;

namespace Cli.Spendfulness.Commands.Personalisation.Users.Switch;

public class UserSwitchCliCommand : ICliCommand
{
    public static class ArugmentNames
    {
        public const string UserName = "user-name";
    }
    
    public string? UserName { get; set; }
}