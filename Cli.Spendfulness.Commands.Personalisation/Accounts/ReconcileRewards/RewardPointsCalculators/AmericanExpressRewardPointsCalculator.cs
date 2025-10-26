using Spendfulness.Database.Accounts;

namespace Cli.Spendfulness.Commands.Personalisation.Accounts.ReconcileRewards.RewardPointsCalculators;

public class AmericanExpressRewardPointsCalculator : IRewardPointsCalculator
{
    private const decimal AmountForConversation = 1000m;
    private const decimal AmountPerThousand = 4.5m;

    public bool CanCalculateForRewardAccount(CustomAccountType accountType)
        => accountType.Name == CustomAccountTypes.AmericanExpressRewards;

    public decimal CalculateForRewardAccount(int pointsValue)
    {
        var howMuchToConvert = pointsValue / AmountForConversation;
        var precisePointsAmount =  howMuchToConvert * AmountPerThousand;
        return Math.Round(precisePointsAmount, 2);
    }
}