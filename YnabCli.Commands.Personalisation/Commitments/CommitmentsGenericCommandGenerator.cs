using Cli.Instructions.Arguments;
using YnabCli.Commands.Generators;
using YnabCli.Commands.Personalisation.Commitments.Find;

namespace YnabCli.Commands.Personalisation.Commitments;

public class CommitmentsGenericCommandGenerator : ICommandGenerator<CommitmentsCommand>
{
    public ICommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        return subCommandName switch
        {
            CommitmentsCommand.SubCommandNames.Find => new CommitmentFindCommand(),
            _ => new CommitmentsCommand(),
        };
    }
}