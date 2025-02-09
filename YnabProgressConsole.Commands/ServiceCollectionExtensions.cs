using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using YnabProgressConsole.Commands.CommandList;

namespace YnabProgressConsole.Commands;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConsoleCommands(this IServiceCollection serviceCollection)
    {
        var commandsAssembly = Assembly.GetAssembly(typeof(ICommand));
        
        return serviceCollection
            .AddMediatRCommandsAndHandlers(commandsAssembly)
            .AddCommandGenerators(commandsAssembly);
    }

    private static IServiceCollection AddMediatRCommandsAndHandlers(this IServiceCollection serviceCollection, Assembly assembly)
        => serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

    private static IServiceCollection AddCommandGenerators(this IServiceCollection serviceCollection, Assembly assembly)
    {
        var implementationTypes = assembly.WhereClassTypesImplement(typeof(ICommandGenerator));
        
        foreach (var implementationType in implementationTypes)
        {
            var genericInterfaceType = implementationType.FirstOrDefaultGenericInterface();

            if (genericInterfaceType is null)
            {
                var implementationTypeName = implementationType.Name;
                var typeName = typeof(ITypedCommandGenerator<>).Name;
                
                throw new ArgumentException($"Type '{implementationTypeName}' does not implement {typeName} interface");
            }
            
            var typeForAssignedCommand = genericInterfaceType.GenericTypeArguments.First();

            var commandNameField = typeForAssignedCommand.GetField(
                nameof(CommandListCommand.CommandName));
            
            var commandNameValue = commandNameField.GetValue(typeForAssignedCommand);
            
            serviceCollection.AddKeyedSingleton(
                typeof(ICommandGenerator),
                commandNameValue,
                implementationType);
        }
        
        return serviceCollection;
    }
}