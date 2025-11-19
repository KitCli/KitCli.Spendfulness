namespace Cli.Commands.Abstractions;

public record ContinuousCliCommand : CliCommand
{
    public List<CliCommandProperty> Properties { get; } = new();
}