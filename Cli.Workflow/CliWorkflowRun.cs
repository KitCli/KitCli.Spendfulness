using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Exceptions;
using Cli.Commands.Abstractions.Outcomes;
using Cli.Commands.Abstractions.Properties;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Abstractions.Validators;
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
    private readonly ICliInstructionValidator _cliInstructionValidator;
    private readonly ICliWorkflowCommandProvider _workflowCommandProvider;
    private readonly IEnumerable<ICliCommandPropertyStrategy> _cliCommandPropertyStrategies;
    private readonly IMediator _mediator;

    public CliWorkflowRun(
        CliWorkflowRunState state,
        ICliInstructionParser cliInstructionParser,
        ICliInstructionValidator cliInstructionValidator,
        ICliWorkflowCommandProvider workflowCommandProvider,
        IEnumerable<ICliCommandPropertyStrategy> cliCommandPropertyStrategies,
        IMediator mediator)
    {
        State = state;
        
        _cliInstructionParser = cliInstructionParser;
        _cliInstructionValidator = cliInstructionValidator;
        _workflowCommandProvider = workflowCommandProvider;
        _cliCommandPropertyStrategies = cliCommandPropertyStrategies;
        _mediator = mediator;
    }
    
    public async ValueTask<CliCommandOutcome> RespondToAsk(string? ask)
    {
        if (!IsEmptyAsk(ask))
        {
            State.ChangeTo(ClIWorkflowRunStateStatus.InvalidAsk);
            return new CliCommandNothingOutcome();
        }
        
        var instruction = _cliInstructionParser.Parse(ask!);
        if (_cliInstructionValidator.IsValidInstruction(instruction))
        {
            State.ChangeTo(ClIWorkflowRunStateStatus.Running, instruction);
        }
        else
        {
            State.ChangeTo(ClIWorkflowRunStateStatus.InvalidAsk);
            return new CliCommandNothingOutcome();
        }

        try
        {
            var command = await PrepareCommand(instruction);
            
            var outcome = await _mediator.Send(command);
            
            HandleAfterCommand(outcome);

            return outcome;
        }
        catch (NoCommandGeneratorException)
        {
            State.ChangeTo(ClIWorkflowRunStateStatus.InvalidAsk);
            return new CliCommandNothingOutcome();
        }
        catch (Exception exception)
        {
            State.ChangeTo(ClIWorkflowRunStateStatus.Exceptional);
            return new CliCommandExceptionOutcome(exception);
        }
        finally
        {
            if (!State.Was(ClIWorkflowRunStateStatus.ReachedReusableOutcome))
            {
                State.ChangeTo(ClIWorkflowRunStateStatus.Finished);
            }
        }
    }
    
    private bool IsEmptyAsk(string? ask) => !string.IsNullOrEmpty(ask);
    
    private bool IsReusableOutcomeKind(CliCommandOutcomeKind kind)
    {
        return kind == CliCommandOutcomeKind.Aggregate;
    }
    
    private async Task<CliCommand> PrepareCommand(CliInstruction instruction)
    {
        var command = _workflowCommandProvider.GetCommand(instruction);

        if (command is not ContinuousCliCommand continuousCommand)
        {
            return command;
        }
        
        // Attach whatever results are from the previous outcomes.
        var properties = State
            .Changes
            .OfType<OutcomeCliWorkflowRunStateChange>()
            .Where(stateChange => _cliCommandPropertyStrategies
                .Any(strategy => strategy.CanCreate(stateChange.Outcome)))
            .Select(stateChange => _cliCommandPropertyStrategies
                .First(strategy => strategy.CanCreate(stateChange.Outcome))
                .CreateProperty(stateChange.Outcome))
            .ToList();
            
        continuousCommand.Properties.AddRange(properties!);

        return command;
    }

    private void HandleAfterCommand(CliCommandOutcome outcome)
    {
        var nextState = IsReusableOutcomeKind(outcome.Kind)
            ? ClIWorkflowRunStateStatus.ReachedReusableOutcome
            : ClIWorkflowRunStateStatus.ReachedFinalOutcome;
            
        State.ChangeTo(nextState, outcome);
    }
}