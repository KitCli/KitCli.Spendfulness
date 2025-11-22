namespace Cli.Commands.Abstractions.Attributes;

// TODO: This is redundant if I just swap out the geneic on the factory interface.
public class FactoryFor(Type commandType) : Attribute
{
    public Type CommandType { get; } = commandType;
}