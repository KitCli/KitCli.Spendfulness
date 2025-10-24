using System.Diagnostics;
using Cli.Instructions.Parsers;
using Cli.Outcomes;
using Cli.Workflow.Abstractions;
using MediatR;
using YnabCli;

namespace Cli;

public class CliWorkflowRun
{
    private readonly CliIo _cliIO;
    private readonly ConsoleInstructionParser _consoleInstructionParser;
    private readonly CliCommandProvider _commandProvider;
    private readonly IMediator _mediator;

    private ClIWorkflowRunState _state { get; set; }
    private List<CliWorkflowRunStateChange> _stateChanges;
    private Stopwatch _stopwatch;

    public CliWorkflowRun(
        CliIo cliIo,
        ConsoleInstructionParser consoleInstructionParser,
        CliCommandProvider commandProvider,
        IMediator mediator)
    {
        _cliIO = cliIo;
        _consoleInstructionParser = consoleInstructionParser;
        _commandProvider = commandProvider;
        _mediator = mediator;
        
        _stopwatch = new Stopwatch();
        _state = ClIWorkflowRunState.Created;
        _stateChanges = [];
    }
    
    public Task<CliCommandOutcome> Execute()
    {
        _stopwatch.Start();
        
        var input = _cliIO.Ask();
        if (string.IsNullOrEmpty(input))
        {
            _cliIO.Say($"Command '{input}' not found");
            UpdateState(ClIWorkflowRunState.NoInput);
            return CreatingNothingOutcome();
        }
        
        var instruction = _consoleInstructionParser.Parse(input);

        try
        {
            UpdateState(ClIWorkflowRunState.Running);

            var command = _commandProvider.GetCommand(instruction);

            return _mediator.Send(command);
        }
        catch (NoInstructionException)
        {
            UpdateState(ClIWorkflowRunState.NoInput);
            return CreatingNothingOutcome();
        }
        catch (NoCommandGeneratorException)
        {
            UpdateState(ClIWorkflowRunState.NoCommand);
            return CreatingNothingOutcome();
        }
        catch (Exception exception)
        {
            _cliIO.Say("Exceptional circumstance ran into.");
            _cliIO.Say(exception.Message);
            
            UpdateState(ClIWorkflowRunState.Exceptional);
            return CreatingNothingOutcome();
        }
        finally
        {
            UpdateState(ClIWorkflowRunState.Finished);
            _stopwatch.Stop();
        }
    }

    private Task<CliCommandOutcome> CreatingNothingOutcome()
    {
        // TODO: Get last state of this run out.
        // In theory, when this is executed, the finally block has not
        // executed so the last state in the state change list
        // will be the appropriate state to passs throgh
        var lastStateChange = _stateChanges.Last();
        var outcome = new CliCommandNothingOutcome(lastStateChange.To);
        return Task.FromResult<CliCommandOutcome>(outcome);
    }
    
    private void UpdateState(ClIWorkflowRunState newState)
    {
        // TODO: I think this state stuff can be done a better way. Maybe a state change abstraction.
        
        if (_state == newState)
        {
            // Already in that state, no mutation needed.
            return;
        }

        if (_state == ClIWorkflowRunState.Created && newState == ClIWorkflowRunState.Running)
        {
            AddStateChange(_state, newState);
            _state = newState;
            
        }

        if (_state == ClIWorkflowRunState.Running && newState == ClIWorkflowRunState.Finished)
        {
            AddStateChange(_state, newState);
            _state = newState;
        }

        throw new Exception($"Invalid Workflow state transition: {_state} > {newState}");
    }

    private void AddStateChange(ClIWorkflowRunState from, ClIWorkflowRunState to)
    {
        var stateChange = new CliWorkflowRunStateChange(from, to);
        _stateChanges.Add(stateChange);
    }
}