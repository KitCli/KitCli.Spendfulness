using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using YnabCli.Commands.Extensions;
using YnabCli.Commands.Organisation.CopyOnBudget;

namespace YnabCli.Commands.Organisation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrganisationCommands(this IServiceCollection serviceCollection)
    {
        var organisationCommandsAssembly = Assembly.GetAssembly(typeof(CopyOnBudgetCommand));
        return serviceCollection.AddCommandsFromAssembly(organisationCommandsAssembly);
    }
}