using Cli.Instructions.Arguments;
using Cli.Instructions.Builders;
using NUnit.Framework;

namespace Cli.Instructions.Tests.InstructionArgumentBuilders;

[TestFixture]
public class IntConsoleInstructionArgumentBuilderTests
{
    private IntConsoleInstructionArgumentBuilder _intConsoleInstructionArgumentBuilder;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _intConsoleInstructionArgumentBuilder = new IntConsoleInstructionArgumentBuilder();
    }
    
    
    [Test]
    public void GivenIntArgumentValue_WhenFor_ShouldReturnTrue()
    {
        var result = _intConsoleInstructionArgumentBuilder.For("1");
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void GivenWrongArgumentValue_WhenFor_ShouldReturnFalse()
    {
        var result = _intConsoleInstructionArgumentBuilder.For("hello test");
        
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void GivenIntArgumentValue_WhenCreate_ShouldReturnInstructionArgument()
    {
        var result = _intConsoleInstructionArgumentBuilder.Create(string.Empty, "1");

        var typed = result as TypedConsoleInstructionArgument<int>;
        
        Assert.That(typed, Is.Not.Null);
        Assert.That(typed.ArgumentValue, Is.EqualTo(1));
    }
}