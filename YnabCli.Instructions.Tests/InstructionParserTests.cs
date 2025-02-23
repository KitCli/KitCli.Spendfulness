using NUnit.Framework;
using YnabCli.Instructions.Arguments;
using YnabCli.Instructions.Builders;
using YnabCli.Instructions.Extraction;
using YnabCli.Instructions.Parsers;

namespace YnabCli.Instructions.Tests;

[TestFixture]
public class InstructionParserTests
{
    private IEnumerable<IInstructionArgumentBuilder> _argumentBuilders;
    private InstructionParser _parser;

    [SetUp]
    public void SetUp()
    {
        _argumentBuilders = new List<IInstructionArgumentBuilder>
        {
            new StringInstructionArgumentBuilder(),
            new IntInstructionArgumentBuilder(),
        };

        _parser = new InstructionParser(_argumentBuilders);
    }

    [Test]
    public void GivenParserTokensWithPrefix_WhenParse_ThenReturnsInstructionWithPrefix()
    {
        var prefix = "/";
        
        var tokens = new InstructionTokenExtraction(
            prefix, 
            string.Empty, 
            string.Empty,
            new Dictionary<string, string?>());
        
        var result = _parser.Parse(tokens);
        
        Assert.That(result.Prefix, Is.EqualTo(prefix));
    }

    [Test]
    public void GivenParserTokensWithName_WhenParse_ThenReturnsInstructionWithName()
    {
        var name = "/";
        
        var tokens = new InstructionTokenExtraction(
            string.Empty,
            name, 
            string.Empty,
            new Dictionary<string, string?>());
        
        var result = _parser.Parse(tokens);
        
        Assert.That(result.Name, Is.EqualTo(name));
    }

    [Test]
    public void GivenExtractionWithSubNae_WhenParse_ThenReturnsInstructionWithSubNae()
    {
        var subname = "help";
        
        var tokens = new InstructionTokenExtraction(
            string.Empty,
            string.Empty, 
            subname,
            new Dictionary<string, string?>());
        
        var result = _parser.Parse(tokens);
        
        Assert.That(result.SubName, Is.EqualTo(subname));
    }

    [Test]
    public void GivenParserWithStringArguments_WhenParse_ThenReturnsInstructionWithStringTypedArguments()
    {
        var argumentName = "argumentName";
        var arvumentValue = "arvumentValue";
        
        var tokens = new InstructionTokenExtraction(
            string.Empty, 
            string.Empty, 
            string.Empty,
            new Dictionary<string, string?>
            {
                { argumentName, arvumentValue }
            });
        
        var result = _parser.Parse(tokens);

        var argument = result.Arguments
            .OfType<TypedInstructionArgument<string>>()
            .FirstOrDefault();
        
        Assert.That(argument, Is.Not.Null);
    }
    
    
    [Test]
    public void GivenParserWithIntArguments_WhenParse_ThenReturnsInstructionWithIntTypedArguments()
    {
        var argumentName = "argumentName";
        var arvumentValue = "1";
        
        var tokens = new InstructionTokenExtraction(
            string.Empty, 
            string.Empty,
            string.Empty,
            new Dictionary<string, string?>
            {
                { argumentName, arvumentValue }
            });
        
        var result = _parser.Parse(tokens);

        var argument = result.Arguments
            .OfType<TypedInstructionArgument<int>>()
            .FirstOrDefault();
        
        Assert.That(argument, Is.Not.Null);
    }
}