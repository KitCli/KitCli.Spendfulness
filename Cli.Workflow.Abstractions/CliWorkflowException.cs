using Cli.Abstractions;
using Cli.Abstractions.Exceptions;

namespace Cli.Workflow.Abstractions;

public class CliWorkflowException(CliWorkflowExceptionCode code, string message)
    : CliException(CliExceptionCode.Command, message)
{
    public new CliWorkflowExceptionCode Code = code;
}