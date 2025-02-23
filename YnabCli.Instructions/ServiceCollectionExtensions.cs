using Microsoft.Extensions.DependencyInjection;
using YnabCli.Instructions.Builders;
using YnabCli.Instructions.Parsers;

namespace YnabCli.Instructions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConsoleInstructions(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IInstructionArgumentBuilder, StringInstructionArgumentBuilder>()
            .AddSingleton<IInstructionArgumentBuilder, DecimalInstructionArgumentBuilder>()
            .AddSingleton<IInstructionArgumentBuilder, IntInstructionArgumentBuilder>()
            .AddSingleton<IInstructionArgumentBuilder, DateOnlyInstructionArgumentBuilder>()
            .AddSingleton<IInstructionArgumentBuilder, BoolInstructionArgumentBuilder>()
            .AddSingleton<LegacyInstructionTokenParser>()
            .AddSingleton<InstructionParser>();

}