using KitCli.Commands.Abstractions;
using KitCli.Commands.Abstractions.Artefacts;
using KitCli.Commands.Abstractions.Factories;
using KitCli.Instructions.Abstractions;
using KitCli.Instructions.Arguments;

namespace SpendfulnessCli.Commands.Organisation.CopyOnBudget;

// TODO: Move me to personalisation class library.
public class CopyOnBudgetCliCommandFactory : ICliCommandFactory<CopyOnBudgetCliCommand>
{
    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
    {
        var accountIdArgument = instruction.Arguments
            .OfRequiredType<Guid>(CopyOnBudgetCliCommand.ArgumentNames.AccountId);
        
        return new CopyOnBudgetCliCommand(accountIdArgument.ArgumentValue);
    }
}