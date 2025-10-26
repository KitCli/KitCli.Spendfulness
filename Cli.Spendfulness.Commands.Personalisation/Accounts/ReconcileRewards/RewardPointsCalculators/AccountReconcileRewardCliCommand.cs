using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Outcomes;
using Spendfulness.Database.Accounts;

namespace Cli.Spendfulness.Commands.Personalisation.Accounts.ReconcileRewards;

public record AccountReconcileRewardCliCommand(string YnabAccountName, int TotalRewardPoints) : ICliCommand
{
    public static class ArgumentNames
    {
        public const string YnabAccountName = "name";
        public const string TotalRewardPoints = "total-points";
    }
}

public class AccountReconcileRewardCliCommandHandler : ICliCommandHandler<AccountReconcileRewardCliCommand>
{
    private readonly CustomAccountAttributeRepository _customAccountAttributeRepository;
    private readonly IEnumerable<IRewardPointsCalculator> _rewardPointsCalculators;

    public AccountReconcileRewardCliCommandHandler(CustomAccountAttributeRepository customAccountAttributeRepository, IEnumerable<IRewardPointsCalculator> rewardPointsCalculators)
    {
        _customAccountAttributeRepository = customAccountAttributeRepository;
        _rewardPointsCalculators = rewardPointsCalculators;
    }

    public async Task<CliCommandOutcome> Handle(AccountReconcileRewardCliCommand command, CancellationToken cancellationToken)
    {
        var attribute = await _customAccountAttributeRepository.Get(command.YnabAccountName, cancellationToken);
        if (attribute is null)
        {
            return new CliCommandOutputOutcome(
                $"{command.YnabAccountName} is not attributed, so cannot be reconciled this way..");
        }

        if (attribute.CustomAccountType is null)
        {
            return new CliCommandOutputOutcome(
                $"{command.YnabAccountName} is not attributed as a reward account, so cannot be reconciled this way..");
        }

        var rewardCalculator = _rewardPointsCalculators
            .FirstOrDefault(rewaardCalculator =>
                rewaardCalculator.CanCalculateForRewardAccount(attribute.CustomAccountType));

        if (rewardCalculator is null)
        {
            return new CliCommandOutputOutcome(
                $"{command.YnabAccountName} is not a supported reward account, so cannot be reconciled this way.");
        }

        var pointsAsAmount = rewardCalculator.CalculateForRewardAccount(command.TotalRewardPoints);
        
        // TODO: Do reconciliation.

        throw new NotImplementedException();
    }
}