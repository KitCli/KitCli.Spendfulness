using KitCli.Commands.Abstractions;

namespace SpendfulnessCli.Commands.Personalisation.Users.Create;

public record CreateUserCliCommand : CliCommand
{
    public static class ArgumentNames
    {
        public const string UserName = "user-name";
    }
    
    public required string UserName { get; set; }
}