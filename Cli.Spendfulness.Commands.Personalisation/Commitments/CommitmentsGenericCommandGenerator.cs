using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;
using Cli.Spendfulness.Commands.Personalisation.Commitments.Find;

namespace Cli.Spendfulness.Commands.Personalisation.Commitments;

public class CommitmentsGenericCommandGenerator : ICommandGenerator<CommitmentsCliCommand>
{
    public ICliCommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        return subCommandName switch
        {
            CommitmentsCliCommand.SubCommandNames.Find => new CommitmentFindCliCommand(),
            _ => new CommitmentsCliCommand(),
        };
    }
}