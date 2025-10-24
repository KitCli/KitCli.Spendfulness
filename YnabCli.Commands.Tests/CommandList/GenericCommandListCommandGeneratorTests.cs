using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;
using YnabCli.Commands.Generators;

namespace YnabCli.Commands.Tests.CommandList;

[TestFixture]
public class GenericCommandListCommandGeneratorTests
{
    [Test]
    public void GivenAnyArguments_WhenGenerate_ReturnsCommandListCommand()
    {
        var generator = new GenericCommandListCommandGenerator();

        var arguments = new List<ConsoleInstructionArgument>();
        
        var result = generator.Generate(string.Empty, arguments);
        
        Assert.That(result, Is.TypeOf<CommandListCommand>());
    }
}