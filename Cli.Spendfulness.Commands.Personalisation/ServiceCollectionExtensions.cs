using System.Reflection;
using Cli.Commands.Abstractions.Extensions;
using Cli.Spendfulness.Commands.Personalisation.Accounts.Identify.ChangeStrategies;
using Cli.Spendfulness.Commands.Personalisation.Accounts.ReconcileRewards;
using Cli.Spendfulness.Commands.Personalisation.Accounts.ReconcileRewards.RewardPointsCalculators;
using Cli.Spendfulness.Commands.Personalisation.Databases;
using Microsoft.Extensions.DependencyInjection;

namespace Cli.Spendfulness.Commands.Personalisation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersonalisationCommands(this IServiceCollection serviceCollection)
    {
        var personalisationCommandsAssembly = Assembly.GetAssembly(typeof(DatabaseCliCommand));

        return serviceCollection
            .AddAccountAttributeChangeStrategies()
            .AddRewardPointsCalculators()
            .AddCommandsFromAssembly(personalisationCommandsAssembly);
    }

    private static IServiceCollection AddAccountAttributeChangeStrategies(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IAccountAttributeChangeStrategy, CustomAccountTypeChangeStrategy>()
            .AddSingleton<IAccountAttributeChangeStrategy, InterestRateChangeStrategy>();

    private static IServiceCollection AddRewardPointsCalculators(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IRewardPointsCalculator, AmericanExpressRewardPointsCalculator>();
}