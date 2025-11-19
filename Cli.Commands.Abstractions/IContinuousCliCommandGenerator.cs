namespace Cli.Commands.Abstractions;

public interface IContinuousCliCommandGenerator<TCommand> : IUnidentifiedContinuousCliCommandGenerator
    where TCommand : CliCommand
{
}