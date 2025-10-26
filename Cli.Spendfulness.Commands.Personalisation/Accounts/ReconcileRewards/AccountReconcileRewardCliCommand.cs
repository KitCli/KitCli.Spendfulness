using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Outcomes;
using Spendfulness.Database.Accounts;

namespace Cli.Spendfulness.Commands.Personalisation.Accounts.ReconcileRewards;

public record AccountReconcileRewardCliCommand(string YnabAccountName, long Points) : ICliCommand
{
    public static class ArgumentNames
    {
        public const string YnabAccountName = "name";
        public const string RewardPoints = "points";
    }
}


public interface IRewardPointsCalculator
{
    bool CanCalculateForRewardAccount();
    
    decimal CalculateForRewardAccount(long pointsValue);
}

public class AccountReconcileRewardCliCommandHandler : ICliCommandHandler<AccountReconcileRewardCliCommand>
{
    private readonly CustomAccountAttributeRepository _customAccountAttributeRepository;
    private readonly IEnumerable<IRewardPointsCalculator> _rewardPointsCalculators;

    public async Task<CliCommandOutcome> Handle(AccountReconcileRewardCliCommand command, CancellationToken cancellationToken)
    {
        var attribute = await _customAccountAttributeRepository.Get(command.YnabAccountName, cancellationToken);
        if (attribute is null)
        {
            return new CliCommandOutputOutcome($"{command.YnabAccountName} is not attributed, so cannot be reconciled this way..");
        }
        
        // TODO: Check if the attribute is a reward account.
        
        // TODO: Do reconciliation.

        throw new NotImplementedException();
    }
}