namespace Cli.Commands.Abstractions.Attributes;

public class CliCommandGeneratorFor(Type cliCommandType) : Attribute
{
    public Type CliCommandType { get; } = cliCommandType;
}