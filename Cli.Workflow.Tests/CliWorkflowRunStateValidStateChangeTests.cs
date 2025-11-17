using Cli.Workflow.Abstractions;
using NUnit.Framework;

namespace Cli.Workflow.Tests;

[TestFixture]
public class CliWorkflowRunStateValidStateChangeTests : CliWorkflowRunStateTests
{
    public static IEnumerable<TestCaseData> ValidStateChanges()
    {
        yield return new TestCaseData(
            Array.Empty<ClIWorkflowRunStateStatus>(),
            ClIWorkflowRunStateStatus.Running
        ).SetName("State can be changed from Created to Running");
        
        yield return new TestCaseData(
            Array.Empty<ClIWorkflowRunStateStatus>(),
            ClIWorkflowRunStateStatus.InvalidAsk
        ).SetName("State can be changed from Created to InvalidAsk");
        
        yield return new TestCaseData(
            new[] { ClIWorkflowRunStateStatus.Running },
            ClIWorkflowRunStateStatus.InvalidAsk
        ).SetName("State can be changed from Running to InvalidAsk");
        
        yield return new TestCaseData(
            new[] { ClIWorkflowRunStateStatus.Running, ClIWorkflowRunStateStatus.InvalidAsk },
            ClIWorkflowRunStateStatus.Finished
        ).SetName("State can be changed from InvalidAsk to Finished");
        
        yield return new TestCaseData(
            new[] { ClIWorkflowRunStateStatus.Running },
            ClIWorkflowRunStateStatus.Exceptional
        ).SetName("State can be changed from Running to Exceptional");
        
        yield return new TestCaseData(
            new[] { ClIWorkflowRunStateStatus.Running, ClIWorkflowRunStateStatus.Exceptional },
            ClIWorkflowRunStateStatus.Finished
        ).SetName("State can be changed from Exceptional to Finished");
        
        yield return new TestCaseData(
            new[] { ClIWorkflowRunStateStatus.Running },
            ClIWorkflowRunStateStatus.Finished
        ).SetName("State can be changed from Running to Finished");
    }
    
    [TestCaseSource(nameof(ValidStateChanges))]
    public void GivenStateIs_WhenChangeTo_CanBeChanged(IEnumerable<ClIWorkflowRunStateStatus> priorStates, ClIWorkflowRunStateStatus stateToChangeTo)
    {
        // Arrange
        var state = GetPreparedState(priorStates);
        
        // Act & Assert
        Assert.DoesNotThrow(() => state.ChangeTo(stateToChangeTo));
    }
    
    [TestCaseSource(nameof(ValidStateChanges))]
    public void GivenStateIsNotInitialized_WhenChangeToCreated_RecordsStateChange(ClIWorkflowRunStateStatus[] priorStates, ClIWorkflowRunStateStatus stateToChangeTo)
    {
        // Arrange
        var state = GetPreparedState(priorStates);
        
        // Act
        state.ChangeTo(stateToChangeTo);
        
        // Assert
        var priorStateChange = priorStates.Any() ? priorStates.Last() : ClIWorkflowRunStateStatus.Created;
        var stateChange = state.Changes.Last();
        
        Assert.That(stateChange, Is.Not.Null);
        Assert.That(stateChange!.From, Is.EqualTo(priorStateChange));
        Assert.That(stateChange.To, Is.EqualTo(stateToChangeTo));
    }
}