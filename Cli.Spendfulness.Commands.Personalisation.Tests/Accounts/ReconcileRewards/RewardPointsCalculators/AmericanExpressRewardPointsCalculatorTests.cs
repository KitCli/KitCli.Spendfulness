using Cli.Spendfulness.Commands.Personalisation.Accounts.ReconcileRewards.RewardPointsCalculators;
using NUnit.Framework;

namespace Cli.Spendfulness.Commands.Personalisation.Tests.Accounts.ReconcileRewards.RewardPointsCalculators;

[TestFixture]
public class AmericanExpressRewardPointsCalculatorTests
{
    [TestCase(1, 0)]
    [TestCase(10, 0.04)]
    [TestCase(100, 0.45)]
    [TestCase(1000, 4.50)]
    [TestCase(10000, 45)]
    [TestCase(100000, 450)]
    public void GivenRewardPointTotal_WhenCalculateForRewardAccount_ReturnsPointsAsAmount(int points, double amount)
    {
        // Given
        var sut = new AmericanExpressRewardPointsCalculator();

        // When
        var result = sut.CalculateForRewardAccount(points);

        // Then
        Assert.That(result, Is.EqualTo(amount));
    }
}