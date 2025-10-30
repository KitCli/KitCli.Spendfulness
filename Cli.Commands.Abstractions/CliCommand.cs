using Cli.Commands.Abstractions.Outcomes;
using Cli.Commands.Abstractions.Properties;
using MediatR;

namespace Cli.Commands.Abstractions;

public abstract record CliCommand(bool isContinuous = false) : IRequest<CliCommandOutcome>
{
    public Dictionary<string, CliCommandProperty> Properties { get; set; } = new();
    public bool IsContinuous { get; } = isContinuous;
}

public abstract record ContinuousCliCommand() : CliCommand(isContinuous: true)
{
}