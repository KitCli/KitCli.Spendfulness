namespace YnabProgressConsole.Commands;

public static class TypeExtensions
{
    public static IEnumerable<Type> WhereClassTypesImplement(this IEnumerable<Type> types, Type thatImplementType)
        => types.Where(assemblyType => assemblyType.IsClass && thatImplementType.IsAssignableFrom(assemblyType));
    
    public static Type? FirstOrDefaultGenericInterface(this Type types)
        => types
            .GetInterfaces()
            .FirstOrDefault(interfaceType => interfaceType.GenericTypeArguments.Length != 0);
}