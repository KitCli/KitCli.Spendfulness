using YnabCli.Commands.CommandList;
using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands.Tests.CommandList;

[TestFixture]
public class CommandListCommandGeneratorTests
{
    [Test]
    public void GivenAnyArguments_WhenGenerate_ReturnsCommandListCommand()
    {
        var generator = new CommandListCommandGenerator();

        var arguments = new List<InstructionArgument>();
        
        var result = generator.Generate(arguments);
        
        Assert.That(result, Is.TypeOf<CommandListCommand>());
    }
}