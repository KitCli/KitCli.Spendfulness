using System.Diagnostics;
using Cli.Commands.Abstractions;
using Cli.Instructions.Parsers;
using Cli.Workflow.Abstractions;
using MediatR;

namespace Cli.Workflow;

public class CliWorkflowRun
{
    private readonly CliWorkflowRunStateManager _stateManager;
    private readonly ConsoleInstructionParser _consoleInstructionParser;
    private readonly CliWorkflowCommandProvider _workflowCommandProvider;
    private readonly IMediator _mediator;
    private readonly Stopwatch _stopwatch;

    public CliWorkflowRun(
        CliWorkflowRunStateManager stateManager,
        ConsoleInstructionParser consoleInstructionParser,
        CliWorkflowCommandProvider workflowCommandProvider,
        IMediator mediator)
    {
        _stateManager = stateManager;
        _consoleInstructionParser = consoleInstructionParser;
        _workflowCommandProvider = workflowCommandProvider;
        _mediator = mediator;
        _stopwatch = new Stopwatch();
    }

    public bool IsValidAsk(string? ask) => string.IsNullOrEmpty(ask);
    
    public async Task<CliCommandOutcome> RespondToAsk(string? ask)
    {
        _stopwatch.Start();
        
        if (!IsValidAsk(ask))
        {
            _stateManager.ChangeTo(ClIWorkflowRunState.InvalidAsk);
            return new CliCommandNothingOutcome();
        }
        
        var instruction = _consoleInstructionParser.Parse(ask!);

        try
        {
            _stateManager.ChangeTo(ClIWorkflowRunState.Running);

            var command = _workflowCommandProvider.GetCommand(instruction);

            return await _mediator.Send(command);
        }
        catch (NoInstructionException)
        {
            _stateManager.ChangeTo(ClIWorkflowRunState.InvalidAsk);
            return new CliCommandNothingOutcome();
        }
        catch (NoCommandGeneratorException)
        {
            _stateManager.ChangeTo(ClIWorkflowRunState.InvalidAsk);
            return new CliCommandNothingOutcome();
        }
        catch (Exception exception)
        {
            _stateManager.ChangeTo(ClIWorkflowRunState.Exceptional);
            return new CliCommandExceptionOutcome(exception);
        }
        finally
        {
            _stateManager.ChangeTo(ClIWorkflowRunState.Finished);
            _stopwatch.Stop();
        }
    }
}