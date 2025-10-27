using Cli.Instructions.Abstractions;
using Cli.Instructions.Indexers;
using Cli.Instructions.Markers;

namespace Cli.Instructions.Extensions;

public static class StringExtensions
{
    public static bool AnyLetters(this string argumentValue)
        => argumentValue
            .ToCharArray()
            .Where(c => !char.IsWhiteSpace(c))
            .Any(char.IsLetter);
    
        public static string Strip(
        this string inputToStrip,
        CliInstructionMarkers markersIdentifyingStripAreas,
        CliInstructionTokenType tokenToStripFor)
    {
        var token = markersIdentifyingStripAreas[tokenToStripFor];
        
        if (!token.Found)
        {
            throw new Exception($"This needs to be handled better!");
        }

        return inputToStrip[token.StartIndex..token.EndIndex];
    }
    
    public static string? StripOptionally(
        this string inputToStrip,
        CliInstructionMarkers markersIdentifyingStripAreas,
        CliInstructionTokenType tokenToStripFor)
    {
        var token = markersIdentifyingStripAreas[tokenToStripFor];
        return !token.Found ? null : inputToStrip[token.StartIndex..token.EndIndex];
    }
    
    public static Dictionary<string, string?> StripArguments(this string? argumentInput)
    {
        if (string.IsNullOrEmpty(argumentInput))
        {
            return new Dictionary<string, string?>();
        }
        
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