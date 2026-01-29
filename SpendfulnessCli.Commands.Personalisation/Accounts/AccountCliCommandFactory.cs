using KitCli.Commands.Abstractions;
using KitCli.Commands.Abstractions.Artefacts;
using KitCli.Commands.Abstractions.Factories;
using KitCli.Instructions.Abstractions;
using KitCli.Instructions.Arguments;

namespace SpendfulnessCli.Commands.Personalisation.Accounts;

public class AccountCliCommandFactory : ICliCommandFactory<AccountCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => instruction.SubInstructionName is null;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
    {
        var accountIdArgument = instruction.Arguments
            .OfRequiredType<Guid>(AccountCliCommand.ArgumentNames.AccountId);

        return new AccountCliCommand(accountIdArgument.ArgumentValue);
    }
}