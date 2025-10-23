using Cli.Instructions.Arguments;
using Cli.Instructions.Builders;
using Cli.Instructions.Extraction;
using Cli.Instructions.Indexers;
using Cli.Instructions.Parsers;
using NUnit.Framework;

namespace Cli.Instructions.Tests;

[TestFixture]
public class ConsoleInstructionParserTests
{
    private ConsoleInstructionTokenIndexer _consoleInstructionTokenIndexer;
    private ConsoleInstructionTokenExtractor _consoleInstructionTokenExtractor;
    private IEnumerable<IConsoleInstructionArgumentBuilder> _instructionArgumentBuilders;
    private ConsoleInstructionParser _parser;

    [SetUp]
    public void SetUp()
    {
        _consoleInstructionTokenIndexer = new ConsoleInstructionTokenIndexer();
        
        _consoleInstructionTokenExtractor = new ConsoleInstructionTokenExtractor();
        
        _instructionArgumentBuilders = new List<IConsoleInstructionArgumentBuilder>
        {
            new StringConsoleInstructionArgumentBuilder(),
            new IntConsoleInstructionArgumentBuilder(),
        };

        _parser = new ConsoleInstructionParser(
            _consoleInstructionTokenIndexer,
            _consoleInstructionTokenExtractor,
            _instructionArgumentBuilders);
    }

    [Test]
    public void GivenParserTokensWithPrefix_WhenParse_ThenReturnsInstructionWithPrefix()
    {
        var result = _parser.Parse("/name");
        
        Assert.That(result.Prefix, Is.EqualTo("/"));
    }

    [Test]
    public void GivenParserTokensWithName_WhenParse_ThenReturnsInstructionWithName()
    {
        var result = _parser.Parse("/name");
        
        Assert.That(result.Name, Is.EqualTo("name"));
    }

    [Test]
    public void GivenExtractionWithSubNae_WhenParse_ThenReturnsInstructionWithSubNae()
    {
        var result = _parser.Parse("/name subname");
        
        Assert.That(result.SubName, Is.EqualTo("subname"));
    }

    [Test]
    public void GivenParserWithStringArguments_WhenParse_ThenReturnsInstructionWithStringTypedArguments()
    {
        var result = _parser.Parse("/command --argument-one hello world");

        var argument = result.Arguments
            .OfType<TypedConsoleInstructionArgument<string>>()
            .FirstOrDefault();
        
        Assert.That(argument, Is.Not.Null);
    }
    
    
    [Test]
    public void GivenParserWithIntArguments_WhenParse_ThenReturnsInstructionWithIntTypedArguments()
    {
        var result = _parser.Parse("/name --argument-one 1");

        var argument = result.Arguments
            .OfType<TypedConsoleInstructionArgument<int>>()
            .FirstOrDefault();
        
        Assert.That(argument, Is.Not.Null);
    }
}