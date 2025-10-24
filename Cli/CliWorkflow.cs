using Cli;
using Cli.Instructions.Parsers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using YnabCli.Abstractions;

namespace YnabCli;

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
        var io = _serviceProvider.GetRequiredService<CliIo>();
        
        var consoleInstructionParser = _serviceProvider.GetRequiredService<ConsoleInstructionParser>();
        
        var commandProvider = _serviceProvider.GetRequiredService<CliCommandProvider>();
        
        // TODO: I'd like to remove the dependency on MediatR one day.
        var mediator = _serviceProvider.GetRequiredService<IMediator>();
        
        var run = new CliWorkflowRun(io, consoleInstructionParser, commandProvider, mediator);
        
        _workflowRuns.Add(run);

        return run;
    }

    public void Stop()
    {
        State = CliWorkflowState.Stopped;
    }
}