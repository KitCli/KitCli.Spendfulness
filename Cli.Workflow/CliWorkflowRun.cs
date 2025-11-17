using Cli.Commands.Abstractions.Exceptions;
using Cli.Commands.Abstractions.Outcomes;
using Cli.Instructions.Parsers;
using Cli.Workflow.Abstractions;
using MediatR;

namespace Cli.Workflow;

// TODO: Cli: I wonder if always attaching this to the command is a great way to add properties?
// And then let implementers of the CLI pass around properties between commands and hooks.
public class CliWorkflowRun
{
    public readonly CliWorkflowRunState State;
    
    private readonly ICliInstructionParser _cliInstructionParser;
    private readonly ICliWorkflowCommandProvider _workflowCommandProvider;
    private readonly IMediator _mediator;

    public CliWorkflowRun(
        CliWorkflowRunState state,
        ICliInstructionParser cliInstructionParser,
        ICliWorkflowCommandProvider workflowCommandProvider,
        IMediator mediator)
    {
        State = state;
        
        _cliInstructionParser = cliInstructionParser;
        _workflowCommandProvider = workflowCommandProvider;
        _mediator = mediator;
    }

    private bool IsValidAsk(string? ask) => !string.IsNullOrEmpty(ask);
    
    public async ValueTask<CliCommandOutcome> RespondToAsk(string? ask)
    {
        if (!IsValidAsk(ask))
        {
            State.ChangeTo(ClIWorkflowRunStateType.InvalidAsk);
            return new CliCommandNothingOutcome();
        }
        
        var instruction = _cliInstructionParser.Parse(ask!);
        
        try
        {
            State.ChangeTo(ClIWorkflowRunStateType.Running);

            var command = _workflowCommandProvider.GetCommand(instruction);

            return await _mediator.Send(command);
        }
        // TODO: CLI - Custom/re-use exception at some point.
        catch (ArgumentNullException)
        {
            State.ChangeTo(ClIWorkflowRunStateType.InvalidAsk);
            return new CliCommandNothingOutcome();
        }
        catch (NoInstructionException)
        {
            State.ChangeTo(ClIWorkflowRunStateType.InvalidAsk);
            return new CliCommandNothingOutcome();
        }
        catch (NoCommandGeneratorException)
        {
            State.ChangeTo(ClIWorkflowRunStateType.InvalidAsk);
            return new CliCommandNothingOutcome();
        }
        catch (Exception exception)
        {
            State.ChangeTo(ClIWorkflowRunStateType.Exceptional);
            return new CliCommandExceptionOutcome(exception);
        }
        finally
        {
            State.ChangeTo(ClIWorkflowRunStateType.Finished);
        }
    }
}