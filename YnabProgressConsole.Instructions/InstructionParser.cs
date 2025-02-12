using YnabProgressConsole.Instructions.InstructionArgumentBuilders;
using YnabProgressConsole.Instructions.InstructionArguments;

namespace YnabProgressConsole.Instructions;

public class InstructionParser(IEnumerable<IInstructionArgumentBuilder> instructionArgumentBuilders)
{
    public Instruction Parse(InstructionTokens tokens)
    {
        var arguments = MapInstructionArguments(tokens.ArgumentTokens);

        return new Instruction(tokens.PrefixToken, tokens.NameToken, arguments);
    }

    private IEnumerable<InstructionArgument> MapInstructionArguments(Dictionary<string, string> argumentTokens)
    {
        foreach (var argumentToken in argumentTokens)
        {
            var argument = instructionArgumentBuilders
                .First(x => x.For(argumentToken.Value))
                .Create(argumentToken.Key, argumentToken.Value);
            
            yield return argument;
        }
    }
}