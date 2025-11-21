namespace Cli.Workflow.Abstractions;

public interface ICliWorkflow
{
    CliWorkflowStatus Status { get; set; }
    
    List<ICliWorkflowRun> Runs { get; set; }
    
    ICliWorkflowRun NextRun();

    void Stop();
}