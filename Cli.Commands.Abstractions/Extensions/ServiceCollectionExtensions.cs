using System.Reflection;
using Cli.Abstractions;
using Cli.Commands.Abstractions.Properties;
using Cli.Instructions.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Cli.Commands.Abstractions.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandsFromAssembly(this IServiceCollection serviceCollection, Assembly? assembly) 
    {
        if (assembly == null)
        {
            throw new NullReferenceException("No Assembly Containing ICommand Implementation");
        }

        return serviceCollection
            .AddCommandGenerators(assembly)
            .AddContinuousCommandGenerators(assembly)
            .AddMediatRCommandsAndHandlers(assembly);
    }

    public static IServiceCollection AddMediatRCommandsAndHandlers(this IServiceCollection serviceCollection, Assembly assembly)
        => serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

    public static IServiceCollection AddCommandGenerators(this IServiceCollection serviceCollection, Assembly assembly)
    {
        var implementationTypes = assembly.WhereClassTypesImplementType(typeof(IUnidentifiedCliCommandGenerator));
        
        foreach (var implementationType in implementationTypes)
        {
            var genericInterfaceType = implementationType.GetRequiredFirstGenericInterface();
            
            var typeForReferencedCommand = genericInterfaceType.GenericTypeArguments.First();
            
            var name = typeForReferencedCommand.Name.Replace(nameof(CliCommand), string.Empty);
            
            var commandName = name.ToLowerSplitString(CliInstructionConstants.DefaultCommandNameSeparator);
            var shorthandCommandName = name.ToLowerTitleCharacters();

            serviceCollection
                .AddKeyedSingleton(
                    typeof(IUnidentifiedCliCommandGenerator),
                    commandName,
                    implementationType)
                .AddKeyedSingleton(
                    typeof(IUnidentifiedCliCommandGenerator),
                    shorthandCommandName,
                    implementationType);
        }
        
        return serviceCollection;
    }

    public static IServiceCollection AddContinuousCommandGenerators(this IServiceCollection serviceCollection, Assembly assembly)
    {
        var continuousImplementationTypes = assembly.WhereClassTypesImplementType(typeof(IUnidentifiedContinuousCliCommandGenerator));
        
        foreach (var implementationType in continuousImplementationTypes)
        {
            var genericInterfaceType = implementationType.GetRequiredFirstGenericInterface();
            
            var typeForReferencedCommand = genericInterfaceType.GenericTypeArguments.First();
            
            var name = typeForReferencedCommand.Name.Replace(nameof(CliCommand), string.Empty);
            
            var commandName = name.ToLowerSplitString(CliInstructionConstants.DefaultCommandNameSeparator);
            var shorthandCommandName = name.ToLowerTitleCharacters();

            serviceCollection
                .AddKeyedSingleton(
                    typeof(IUnidentifiedContinuousCliCommandGenerator),
                    commandName,
                    implementationType)
                .AddKeyedSingleton(
                    typeof(IUnidentifiedContinuousCliCommandGenerator),
                    shorthandCommandName,
                    implementationType);
        }
        
        return serviceCollection;
    }
    
    public static IServiceCollection AddCommandPropertiesFromAssembly(this IServiceCollection serviceCollection, Assembly? assembly)
    {
        if (assembly == null)
        {
            throw new NullReferenceException("No Assembly Containing ICommand Implementation");
        }
        
        // get all aggregates, generate property strategies for them?

        var possibleAggregatorTypes = assembly
            .GetTypes()
            .Where(type => !type.IsAbstract);
        
        foreach (var possibleAggregatorType in possibleAggregatorTypes)
        {
            var aggregatorType = possibleAggregatorType.GetSuperclassGenericOf(typeof(CliAggregator<>));
            if (aggregatorType is null)
            {
                continue;
            }
            
            var typeForReferencedAggregate = aggregatorType.GenericTypeArguments.First();
        
            var strategyType = typeof(AggregatorCliCommandPropertyStrategy<>).MakeGenericType(typeForReferencedAggregate);
            
            var instance = Activator.CreateInstance(strategyType) as ICliCommandPropertyStrategy;

            serviceCollection
                .AddSingleton<ICliCommandPropertyStrategy>(instance);
        }
        
        return serviceCollection;
    }
}