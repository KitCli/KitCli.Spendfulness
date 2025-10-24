using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;
using YnabCli.Commands.Generators;

namespace YnabCli.Commands.Organisation.CopyOnBudget;

public class CopyOnBudgetCommandGenerator : ICommandGenerator<CopyOnBudgetCommand>
{
    public ICommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        var accountIdArgument = arguments.OfRequiredType<Guid>(CopyOnBudgetCommand.ArgumentNames.AccountId);

        return new CopyOnBudgetCommand(accountIdArgument.ArgumentValue);
    }
}