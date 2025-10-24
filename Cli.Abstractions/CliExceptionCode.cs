namespace Cli.Abstractions;

public enum CliExceptionCode
{
    // Extensions
    Workflow,
    Command,
    
    // Roots
    NoCommandGenerator,
    NoInstruction
}