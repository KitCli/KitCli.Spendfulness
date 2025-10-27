using Cli.Commands.Abstractions.Outcomes;
using Cli.Workflow;
using Cli.Workflow.Abstractions;

namespace Cli;

// TODO: Write unit tests.
public abstract class OriginalCli
{
    public readonly CliWorkflow Workflow;
    public readonly CliCommandOutcomeIo Io;

    protected OriginalCli(CliWorkflow workflow, CliCommandOutcomeIo io)
    {
        Workflow = workflow;
        Io = io;
    }
    
    public async Task Run()
    { 
        OnSessionStart();
        
        while (Workflow.Status != CliWorkflowStatus.Stopped)
        {
            var run = Workflow.CreateRun();
            
            OnRunCreated(run);
            
            var ask = Io.Ask();
            
            var runTask =  run.RespondToAsk(ask);
            
            OnRunStarted(run, ask);

            var outcome = await runTask;
            
            Io.Say(outcome);
            
            OnRunComplete(run, outcome);
        }
        
        OnSessionEnd(Workflow.Runs);
    }

    protected virtual void OnSessionStart()
    {
    }

    protected virtual void OnRunCreated(CliWorkflowRun run)
    {
    }

    protected virtual void OnRunStarted(CliWorkflowRun run, string ask)
    {
    }

    protected virtual void OnRunComplete(CliWorkflowRun run, CliCommandOutcome outcome)
    {
    }
    
    protected virtual void OnSessionEnd(List<CliWorkflowRun> runs)
    {
    }
}