using YnabCli.Instructions.Builders;
using YnabCli.Instructions.Extraction;

namespace YnabCli.Instructions.Parsers;

public class InstructionParser(IEnumerable<IInstructionArgumentBuilder> instructionArgumentBuilders)
{
    public Instruction Parse(InstructionTokenExtraction tokenExtraction)
    {
        var arguments = tokenExtraction.ArgumentTokens
            .Select(token => instructionArgumentBuilders
                .First(builder => builder.For(token.Value))
                .Create(token.Key, token.Value));

        return new Instruction(
            tokenExtraction.PrefixToken,
            tokenExtraction.NameToken,
            tokenExtraction.SubNameToken,
            arguments);
    }
}