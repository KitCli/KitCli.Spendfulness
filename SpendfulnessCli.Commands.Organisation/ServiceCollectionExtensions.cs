using System.Reflection;
using Cli.Commands.Abstractions.Extensions;
using SpendfulnessCli.Commands.Organisation.CopyOnBudget;
using Microsoft.Extensions.DependencyInjection;

namespace SpendfulnessCli.Commands.Organisation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpendfulnessOrganisationCommands(this IServiceCollection serviceCollection)
    {
        var organisationCommandsAssembly = Assembly.GetAssembly(typeof(CopyOnBudgetCliCommand));
        return serviceCollection.AddCommandsFromAssembly(organisationCommandsAssembly);
    }
}