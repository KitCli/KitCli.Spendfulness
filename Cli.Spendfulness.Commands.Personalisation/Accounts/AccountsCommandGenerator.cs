using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;
using Cli.Spendfulness.Commands.Personalisation.Accounts.Identify;
using Cli.Spendfulness.Commands.Personalisation.Accounts.ReconcileRewards;

namespace Cli.Spendfulness.Commands.Personalisation.Accounts;

public class AccountsCommandGenerator : ICommandGenerator<AccountsCliCommand>
{
    public ICliCommand Generate(CliInstruction instruction)
        => instruction.SubInstructionName switch
        {
            AccountsCliCommand.SubCommandNames.Identify => GenerateIdentifyCommand(instruction.Arguments),
            AccountsCliCommand.SubCommandNames.ReconcileRewards => GenerateReconcileRewardsCommand(instruction.Arguments),
            _ => new AccountsCliCommand()
        };
    
    private  AccountsIdentifyCliCommand GenerateIdentifyCommand(List<CliInstructionArgument> arguments)
    {
        var nameArgument = arguments.OfRequiredType<string>(AccountsIdentifyCliCommand.ArgumentNames.Name);
        var typeArgument = arguments.OfType<string>(AccountsIdentifyCliCommand.ArgumentNames.Type);
        var interestRateArgument = arguments.OfType<decimal>(AccountsIdentifyCliCommand.ArgumentNames.InterestRate);

        return new AccountsIdentifyCliCommand(
            nameArgument.ArgumentValue,
            typeArgument?.ArgumentValue,
            interestRateArgument?.ArgumentValue);
    }

    private AccountReconcileRewardCliCommand GenerateReconcileRewardsCommand(List<CliInstructionArgument> arguments)
    {
        var nameArgument = arguments.OfRequiredType<string>(AccountReconcileRewardCliCommand.ArgumentNames.YnabAccountName);
        var rewardPointsArgument = arguments.OfRequiredType<long>(AccountReconcileRewardCliCommand.ArgumentNames.RewardPoints);
        
        return new AccountReconcileRewardCliCommand(
            nameArgument.ArgumentValue,
            rewardPointsArgument.ArgumentValue);
    }
}