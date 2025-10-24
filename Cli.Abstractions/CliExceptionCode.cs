namespace Cli.Abstractions;

public enum CliExceptionCode
{
    Workflow,
    Instruction,
    Command,
    Custom,
    
    NoCommandGenerator,
    NoInstruction
}