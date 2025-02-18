namespace YnabCli.Instructions;

public class InstructionTokenParser
{
    private const string DefaultNamePrefix = "/";
    private const string DefaultArgumentPrefix = "--";
    private const char DefaultSpaceCharacter = ' ';
    
    public InstructionTokens Parse(string terminalInput)
    {
        var includesInputBeyondNameToken = terminalInput.Contains(DefaultSpaceCharacter);
        
        var indexAfterNameToken = includesInputBeyondNameToken 
            ? terminalInput.IndexOf(' ')  // At the end of the command name
            : terminalInput.Length; // The end of the input will be the end of the command name/
        
        var namePrefixToken = ParseNamePrefixToken(terminalInput);
        
        var nameToken = ParseNameToken(terminalInput, indexAfterNameToken);
        
        var argumentTokens = ParseArgumentTokens(
            terminalInput, indexAfterNameToken, includesInputBeyondNameToken);

        return new InstructionTokens(namePrefixToken, nameToken, argumentTokens);
    }

    private string? ParseNamePrefixToken(string terminalInput)
    {
        var indexOfNamePrefixToken = terminalInput.IndexOf(DefaultNamePrefix, StringComparison.CurrentCulture);
        return indexOfNamePrefixToken == 0 ? terminalInput[..1] : null;
    }

    private string ParseNameToken(string terminalInput, int indexAfterNameToken)
    {
        var indexOfEndOfNameToken = indexAfterNameToken - 1;
        return terminalInput.Substring(1, indexOfEndOfNameToken);
    }

    private Dictionary<string, string> ParseArgumentTokens(
        string terminalInput, int indexAfterNameToken, bool includesInputBeyondNameToken)
    {
        var indexOfStartOfRemainingInput = indexAfterNameToken + 1;
        
        var remainingTerminalInput = includesInputBeyondNameToken
            ? terminalInput.Substring(indexOfStartOfRemainingInput)
            : string.Empty;

        return remainingTerminalInput
            .Split(DefaultArgumentPrefix)
            .Where(argumentInput => argumentInput != string.Empty)
            .Select(argumentInput => SplitArgumentInput(argumentInput))
            .ToDictionary(kvp => kvp.Key, split => split.Value);
    }

    private KeyValuePair<string, string> SplitArgumentInput(string terminalArgumentInput)
    {
        // e.g. --payee-name Subway Something Something
        var firstIndexOfSpace = terminalArgumentInput.IndexOf(DefaultSpaceCharacter);
        
        var argumentName = terminalArgumentInput.Substring(0, firstIndexOfSpace);
        
        var indexOfStartOfArgumentValue = firstIndexOfSpace + 1;
        var argumentValue = terminalArgumentInput
            .Substring(indexOfStartOfArgumentValue)
            .Trim();
        
        return new KeyValuePair<string, string>(argumentName, argumentValue);
    }
}