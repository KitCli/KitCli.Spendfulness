using Cli.Commands.Abstractions;

namespace YnabCli.Commands.Generators;

// ReSharper disable once UnusedTypeParameter
public interface ICommandGenerator<TCommand> : IGenericCommandGenerator where TCommand : ICommand
{
    // This is helping us with reflection for DI.
}