using YnabCli.Commands.Generators;
using YnabCli.Commands.Personalisation.Commitments.Find;
using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands.Personalisation.Commitments;

public class CommitmentsCommandGenerator : ICommandGenerator, ITypedCommandGenerator<CommitmentsCommand>
{
    public ICommand Generate(string? subCommandName, List<InstructionArgument> arguments)
    {
        return subCommandName switch
        {
            CommitmentsCommand.SubCommandNames.Find => new CommitmentFindCommand(),
            _ => new CommitmentsCommand(),
        };
    }
}