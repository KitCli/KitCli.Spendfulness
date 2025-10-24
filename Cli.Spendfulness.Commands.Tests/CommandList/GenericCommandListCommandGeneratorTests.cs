using Cli.Instructions.Abstractions;
using Cli.Spendfulness.Commands.Generators;

namespace Cli.Spendfulness.Commands.Tests.CommandList;

[TestFixture]
public class GenericCommandListCommandGeneratorTests
{
    [Test]
    public void GivenAnyArguments_WhenGenerate_ReturnsCommandListCommand()
    {
        var generator = new GenericCommandListCommandGenerator();

        var instruction = new CliInstruction(
            string.Empty,
            string.Empty,
            string.Empty, 
            []);
        
        var result = generator.Generate(instruction);
        
        Assert.That(result, Is.TypeOf<CliCommandListCliCommand>());
    }
}