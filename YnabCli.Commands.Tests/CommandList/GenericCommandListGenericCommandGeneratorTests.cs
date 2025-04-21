using YnabCli.Commands.Generators;
using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands.Tests.CommandList;

[TestFixture]
public class GenericCommandListGenericCommandGeneratorTests
{
    [Test]
    public void GivenAnyArguments_WhenGenerate_ReturnsCommandListCommand()
    {
        var generator = new GenericCommandListGenericCommandGenerator();

        var arguments = new List<InstructionArgument>();
        
        var result = generator.Generate(string.Empty, arguments);
        
        Assert.That(result, Is.TypeOf<CommandListCommand>());
    }
}