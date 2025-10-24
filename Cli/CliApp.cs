using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Io;
using Cli.Commands.Abstractions.Outcomes;
using Cli.Workflow;
using Cli.Workflow.Abstractions;

namespace Cli;

public abstract class CliApp
{
    private readonly CliWorkflow _cliWorkflow;
    private readonly CliCommandOutcomeIo _cliCommandOutcomeIo;

    protected CliApp(CliWorkflow cliWorkflow, CliCommandOutcomeIo cliCommandOutcomeIo)
    {
        _cliWorkflow = cliWorkflow;
        _cliCommandOutcomeIo = cliCommandOutcomeIo;
    }
    
    public async Task Run()
    { 
        OnRun(_cliWorkflow, _cliCommandOutcomeIo);
        
        while (_cliWorkflow.State != CliWorkflowState.Stopped)
        {
            var cliWorkflowRun = _cliWorkflow.CreateRun();
            
            OnRunCreated(cliWorkflowRun, _cliCommandOutcomeIo);
            
            var ask = _cliCommandOutcomeIo.Ask();
            
            var cliWorkflowRunTask =  cliWorkflowRun.RespondToAsk(ask);
            
            OnRunStarted(cliWorkflowRun, _cliCommandOutcomeIo);

            var outcome = await cliWorkflowRunTask;
            
            _cliCommandOutcomeIo.Say(outcome);
        }
    }

    protected virtual void OnRun(CliWorkflow cliWorkflow, CliIo cliIo)
    {
    }

    protected virtual void OnRunCreated(CliWorkflowRun cliWorkflowRun, CliIo cliIo)
    {
    }

    protected virtual void OnRunStarted(CliWorkflowRun cliWorkflowRun, CliIo cliIo)
    {
    }
}