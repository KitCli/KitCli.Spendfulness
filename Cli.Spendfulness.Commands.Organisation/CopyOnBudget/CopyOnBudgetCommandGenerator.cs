using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;

namespace Cli.Spendfulness.Commands.Organisation.CopyOnBudget;

public class CopyOnBudgetCommandGenerator : ICommandGenerator<CopyOnBudgetCliCommand>
{
    public ICliCommand Generate(CliInstruction instruction)
    {
        var accountIdArgument = instruction.Arguments
            .OfRequiredType<Guid>(CopyOnBudgetCliCommand.ArgumentNames.AccountId);
        
        return new CopyOnBudgetCliCommand(accountIdArgument.ArgumentValue);
    }
}