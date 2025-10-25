namespace Cli.Commands.Abstractions;

// ReSharper disable once UnusedTypeParameter
public interface ICommandGenerator<TCommand> : IGenericCommandGenerator where TCommand : ICliCommand
{
    // This is helping us with reflection for DI.
}