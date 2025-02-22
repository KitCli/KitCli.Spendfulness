using Microsoft.Extensions.DependencyInjection;
using YnabCli.Instructions.InstructionArgumentBuilders;

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
            .AddSingleton<InstructionTokenParser>()
            .AddSingleton<InstructionParser>();

}