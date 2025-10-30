using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Exceptions;
using Cli.Commands.Abstractions.Outcomes;
using Cli.Commands.Abstractions.Properties;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Parsers;
using Cli.Workflow.Abstractions;
using MediatR;

namespace Cli.Workflow;

// TODO: Write unit tests.
// TODO: Cli: I wonder if always attaching this to the command is a great way to add properties?
// And then let implementers of the CLI pass around properties between commands and hooks.
public class CliWorkflowRun
{
    public readonly CliWorkflowRunState State;
    public readonly List<CliInstruction> Instructions;
    public readonly Dictionary<string, CliCommandProperty> Properties;
    
    private readonly CliInstructionParser _cliInstructionParser;
    private readonly CliWorkflowCommandProvider _workflowCommandGeneratorProvider;
    private readonly IMediator _mediator;

    public CliWorkflowRun(
        CliWorkflowRunState state,
        List<CliInstruction> instructions,
        Dictionary<string, CliCommandProperty> properties,
        CliInstructionParser cliInstructionParser,
        CliWorkflowCommandProvider workflowCommandGeneratorProvider,
        IMediator mediator)
    {
        State = state;
        Instructions = instructions;
        Properties = properties;

        _cliInstructionParser = cliInstructionParser;
        _workflowCommandGeneratorProvider = workflowCommandGeneratorProvider;
        _mediator = mediator;
    }

    private bool IsValidAsk(string? ask) => !string.IsNullOrEmpty(ask);
    
    public async Task<CliCommandOutcome> RespondToAsk(string? ask)
    {
        var needsToContinue = State.Is(ClIWorkflowRunStateType.NeedsToContinue);
        
        if (!needsToContinue)
        {
            // If it's already running, its not created!
            State.ChangeTo(ClIWorkflowRunStateType.Created);
        }
        
        // Do process as normal.
        if (!IsValidAsk(ask))
        {
            State.ChangeTo(ClIWorkflowRunStateType.InvalidAsk);
            return new CliCommandNothingOutcome();
        }

        try
        {
            State.ChangeTo(ClIWorkflowRunStateType.Running);
            
            var instruction = _cliInstructionParser.Parse(ask!);
            
            var command = PrepareCommand(instruction);
            
            return await ExecuteCommand(command);
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
            var instruction = _cliInstructionParser.Parse(ask!);
            var commandGenerator = _workflowCommandGeneratorProvider.Provide(instruction);
            var command = commandGenerator.Generate(instruction);
            
            // TODO: Not neccessarily a dictator of continuation.
            var nextState = command.IsContinuous
                ? ClIWorkflowRunStateType.NeedsToContinue
                : ClIWorkflowRunStateType.Finished;
                        
            State.ChangeTo(nextState);
        }
    }

    public CliCommand PrepareCommand(CliInstruction nextInstruction)
    {
        // TODO: If re-running, get the old generator. If not re-running, get a new one.
        var instructionToGenerateCommandWith = State.Is(ClIWorkflowRunStateType.NeedsToContinue)
            ? Instructions.Last()
            : nextInstruction;
        
        var commandGenerator = _workflowCommandGeneratorProvider.Provide(instructionToGenerateCommandWith);
        
        var command = commandGenerator.Generate(nextInstruction);

        // Attach already existing properties to newly generate command.
        command.Properties = Properties;
        
        return command;
    }

    private async Task<CliCommandOutcome> ExecuteCommand<TCliCommand>(TCliCommand command) where TCliCommand : CliCommand
    {
        var outcome = await _mediator.Send(command);
        
        if (outcome is CliCommandPropertiesOutcome propertiesOutcome)
        {
            // Overwrite existing properties with new ones in case they were changed.
            foreach (var property in propertiesOutcome.Properties)
            {
                Properties[property.PropertyKey] = property;
            }
        }
        
        return outcome;
    }
}