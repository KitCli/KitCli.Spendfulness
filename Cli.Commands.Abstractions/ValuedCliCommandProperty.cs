namespace Cli.Commands.Abstractions;

public record ValuedCliCommandProperty<TValue>(TValue Value) : CliCommandProperty;