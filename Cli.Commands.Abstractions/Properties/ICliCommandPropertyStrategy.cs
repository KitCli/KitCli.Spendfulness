using Cli.Commands.Abstractions.Outcomes;

namespace Cli.Commands.Abstractions.Properties;

public interface ICliCommandPropertyStrategy
{
    public bool CanCreate(CliCommandOutcome outcome);
    
    CliCommandProperty CreateProperty(CliCommandOutcome outcome);
}