using Cli.Commands.Abstractions;

namespace Cli.Workflow.Commands.MissingOutcomes;

public record MissingOutcomesCliCommand(string[] MissingOutcomeNames) : CliCommand
{
    public readonly string[] MissingOutcomeNames = MissingOutcomeNames;
}