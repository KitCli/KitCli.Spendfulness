using Spendfulness.Database.Accounts;

namespace Cli.Spendfulness.Commands.Personalisation.Accounts.ReconcileRewards;

public interface IRewardPointsCalculator
{
    bool CanCalculateForRewardAccount(CustomAccountType accountType);
    
    decimal CalculateForRewardAccount(int pointsValue);
}