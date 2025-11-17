using Cli.Workflow.Abstractions;
using NUnit.Framework;

namespace Cli.Workflow.Tests;

public class CliWorkflowRunStateInvalidStateChangeTests : CliWorkflowRunStateTests
{
    public static IEnumerable<TestCaseData> InvalidStateChanges()
    {
        yield return new TestCaseData(
            Enumerable.Empty<ClIWorkflowRunStateStatus>(),
            ClIWorkflowRunStateStatus.Created
        ).SetName("State cannot be Created when already Created");
        
        yield return new TestCaseData(
            new [] { ClIWorkflowRunStateStatus.Running },
            ClIWorkflowRunStateStatus.Running
        ).SetName("State cannot be Running when already Running");
        
        yield return new TestCaseData(
            new [] { ClIWorkflowRunStateStatus.Running, ClIWorkflowRunStateStatus.InvalidAsk},
            ClIWorkflowRunStateStatus.InvalidAsk
        ).SetName("State cannot be InvalidAsk when already InvalidAsk");
        
        yield return new TestCaseData(
            Enumerable.Empty<ClIWorkflowRunStateStatus>(),
            ClIWorkflowRunStateStatus.Exceptional
        ).SetName("State cannot be Exceptional when not NotInitialized");
        
        yield return new TestCaseData(
            new [] { ClIWorkflowRunStateStatus.Running, ClIWorkflowRunStateStatus.Exceptional },
            ClIWorkflowRunStateStatus.Exceptional
        ).SetName("State cannot be InvalidAsk when already Exceptional");
        
        yield return new TestCaseData(
            Enumerable.Empty<ClIWorkflowRunStateStatus>(),
            ClIWorkflowRunStateStatus.Finished
        ).SetName("State cannot be Finished when not NotInitialized");
        
        yield return new TestCaseData(
            new [] { ClIWorkflowRunStateStatus.Running, ClIWorkflowRunStateStatus.Finished },
            ClIWorkflowRunStateStatus.Finished
        ).SetName("State cannot be Finished when already Finished");
    }
    
    [TestCaseSource(nameof(InvalidStateChanges))]
    public void GivenStateIs_WhenChangedTo_CannotBeChanged(IEnumerable<ClIWorkflowRunStateStatus> priorStates, ClIWorkflowRunStateStatus stateToChangeTo)
    {
        // Arrange
        var state = GetPreparedState(priorStates);
        
        // Act & Assert
        Assert.Throws<ImpossibleStateChangeException>(() => state.ChangeTo(stateToChangeTo));
    }
}