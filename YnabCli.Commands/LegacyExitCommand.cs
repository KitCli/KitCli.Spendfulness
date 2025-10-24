using Cli.Commands.Abstractions;

namespace YnabCli.Commands;

[Obsolete("Need to re-create this to change Workflow State.")]
public class LegacyExitCommand : ICommand
{
    public const string CommandName = "exit";
    public const string ShorthandCommandName = "e";
}