using YnabCli.Commands.Generators;
using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands.Organisation.CopyOnBudget;

public class CopyOnBudgetCommandGenerator : ICommandGenerator<CopyOnBudgetCommand>
{
    public ICommand Generate(string? subCommandName, List<InstructionArgument> arguments)
    {
        var accountIdArgument = arguments.OfRequiredType<Guid>(CopyOnBudgetCommand.ArgumentNames.AccountId);

        return new CopyOnBudgetCommand(accountIdArgument.ArgumentValue);
    }
}