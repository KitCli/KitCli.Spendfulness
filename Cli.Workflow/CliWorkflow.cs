using Cli.Instructions.Abstractions.Validators;
using Cli.Instructions.Parsers;
using Cli.Workflow.Abstractions;
using Cli.Workflow.Commands;
using Cli.Workflow.Run;
using Cli.Workflow.Run.State;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cli.Workflow;

/// <summary>
/// State machine of a command line interface.
/// </summary>
public class CliWorkflow(IServiceProvider serviceProvider) : ICliWorkflow
{
    public List<ICliWorkflowRun> Runs { get; } = [];
    public CliWorkflowStatus Status { get; set; } = CliWorkflowStatus.Started;


    /// <summary>
    /// Create a new run, a sub-state machine of an individual execution.
    /// </summary>
    /// <returns>A sub-state mchine of an individual execution.</returns>
    public ICliWorkflowRun NextRun()
    {
        var lastRunToAchieveReusableOutcome = Runs
            .LastOrDefault(run => run.State.WasChangedToReusableOutcome());

        return lastRunToAchieveReusableOutcome ?? CreateNewRun();
    }

    /// <summary>
    /// Close the state machine.
    /// </summary>
    public void Stop()
    {
        Status = CliWorkflowStatus.Stopped;
    }
    
    private ICliWorkflowRun CreateNewRun()
    {
        var state = new CliWorkflowRunState();
        
        var instructionParser = serviceProvider.GetRequiredService<ICliInstructionParser>();

        var instructionValidator = serviceProvider.GetRequiredService<ICliInstructionValidator>();
        
        var commandProvider = serviceProvider.GetRequiredService<ICliWorkflowCommandProvider>();
        
        var mediator = serviceProvider.GetRequiredService<IMediator>();
        
        var run = new CliWorkflowRun(
            state,
            instructionParser,
            instructionValidator,
            commandProvider,
            mediator);
        
        Runs.Add(run);

        return run;
    }
}