using Cli.Commands.Abstractions;

namespace YnabCli.Commands.Personalisation.Commitments;

public class CommitmentsCommand : ICommand
{
    public static class SubCommandNames
    {
        public const string Find = "find";
    }
}