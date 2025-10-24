using Cli.Instructions.Parsers;
using Cli.Workflow.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cli.Workflow;

public class CliWorkflow
{
    private readonly IServiceProvider _serviceProvider;
    private List<CliWorkflowRun> _workflowRuns = [];
    
    public CliWorkflowState State = CliWorkflowState.Started;

    public CliWorkflow(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public CliWorkflowRun CreateRun()
    {
        // TODO: CLI - Store this somewhere?
        var state = new CliWorkflowRunState();
        
        var instructionParser = _serviceProvider.GetRequiredService<CliInstructionParser>();
        
        var commandProvider = _serviceProvider.GetRequiredService<CliWorkflowCommandProvider>();
        
        var mediator = _serviceProvider.GetRequiredService<IMediator>();
        
        var run = new CliWorkflowRun(state, instructionParser, commandProvider, mediator);
        
        _workflowRuns.Add(run);

        return run;
    }

    public void Stop()
    {
        State = CliWorkflowState.Stopped;
    }
}