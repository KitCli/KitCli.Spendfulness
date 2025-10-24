using System.Reflection;
using Cli.Commands.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using YnabCli.Commands.Extensions;
using YnabCli.Commands.Personalisation.Databases;

namespace YnabCli.Commands.Personalisation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersonalisationCommands(this IServiceCollection serviceCollection)
    {
        var personalisationCommandsAssembly = Assembly.GetAssembly(typeof(DatabaseCommand));
        return serviceCollection.AddCommandsFromAssembly(personalisationCommandsAssembly);
    }
}