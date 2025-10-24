using Cli.Instructions.Abstractions;
using Cli.Instructions.Indexers;

namespace Cli.Instructions.Extraction;

public class CliInstructionTokenExtractor
{
    public CliInstructionTokenExtraction Extract(CliInstructionTokenIndexes indexes, string terminalInput)
    {
        var prefixToken = ExtractPrefixToken(indexes, terminalInput);
        var nameToken = ExtractNameToken(indexes, terminalInput);
        var subNameToken = ExtractSubNameToken(indexes, terminalInput);
        var argumentTokens = ExtractArgumentTokens(indexes, terminalInput);
        
        return new CliInstructionTokenExtraction(prefixToken, nameToken, subNameToken, argumentTokens);
    }

    private string ExtractPrefixToken(CliInstructionTokenIndexes indexes, string terminalInput)
    {
        if (!indexes.PrefixTokenIndexed)
        {
            throw new CliInstructionException(
                CliInstructionExceptionCode.NoInstructionPrefix,
                $"Instructions must contain a {CliInstructionConstants.DefaultNamePrefix}");
        }

        return terminalInput[indexes.PrefixTokenStartIndex..indexes.PrefixTokenEndIndex];
    }

    private string ExtractNameToken(CliInstructionTokenIndexes indexes, string terminalInput)
    {
        if (!indexes.NameTokenIndexed)
        {
            throw new CliInstructionException(
                CliInstructionExceptionCode.NoInstructionName,
                $"Instructions must have a name");
        }

        return terminalInput[indexes.NameTokenStartIndex..indexes.NameTokenEndIndex];
    }

    private string? ExtractSubNameToken(CliInstructionTokenIndexes indexes, string terminalInput)
    {
        if (!indexes.SubNameTokenIndexed)
        {
            return null;
        }

        return terminalInput[indexes.SubNameStartIndex..indexes.SubNameEndIndex];
    }
    
    private static Dictionary<string, string?> ExtractArgumentTokens(CliInstructionTokenIndexes indexes, string terminalInput)
    {
        if (!indexes.ArgumentTokensIndexed)
        {
            return new Dictionary<string, string?>();
        }
        
        var argumentInput = terminalInput[indexes.ArgumentTokensStartIndex..indexes.ArgumentTokensEndIndex];

        var argumentTokens = argumentInput.Split(CliInstructionConstants.DefaultArgumentPrefix);
        
        var validArgumentTokens = argumentTokens
            .Where(i => !string.IsNullOrWhiteSpace(i))
            .Select(i => i.Trim());
        
        var parsedArgumentTokens = validArgumentTokens.Select(ParseArgumentInput);
        
        return parsedArgumentTokens.ToDictionary(token => token.Key, token => token.Value);
    }
    
    private static KeyValuePair<string, string?> ParseArgumentInput(string terminalArgumentInput)
    {
        // e.g. --payee-name Subway Something Something
        var firstIndexOfSpace = terminalArgumentInput.IndexOf(CliInstructionConstants.DefaultSpaceCharacter);
        
        var argumentNameEndIndex = firstIndexOfSpace == -1
            ? terminalArgumentInput.Length
            : firstIndexOfSpace;

        var argumentName = terminalArgumentInput.Substring(0, argumentNameEndIndex);

        var argumentValue = firstIndexOfSpace == -1
            ? null
            : terminalArgumentInput[argumentNameEndIndex..].Trim();
        
        return new KeyValuePair<string, string?>(argumentName, argumentValue);
    }
}