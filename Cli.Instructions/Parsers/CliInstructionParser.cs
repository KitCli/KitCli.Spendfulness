using Cli.Instructions.Abstractions;
using Cli.Instructions.Builders;
using Cli.Instructions.Extensions;
using Cli.Instructions.Indexers;
using Cli.Instructions.Markers;

namespace Cli.Instructions.Parsers;

public class CliInstructionParser(IEnumerable<ICliInstructionArgumentBuilder> argumentBuilders)
{
    public CliInstruction Parse(string terminalInput)
    {
        var markers = CliInstructionMarkers.For(terminalInput);
        
        var tokens = markers.GetTokensFrom(terminalInput);
        
        var arguments = tokens
            .Arguments
            .StripArguments()
            .Select(token => argumentBuilders
                .First(builder => builder.For(token.Value))
                .Create(token.Key, token.Value))
            .ToList();
        
        return new CliInstruction(
            tokens.Prefix,
            tokens.Name,
            tokens.SubName,
            arguments);
    }
}