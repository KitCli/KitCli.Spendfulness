using Cli.Instructions.Builders;
using Cli.Instructions.Extraction;
using Cli.Instructions.Indexers;
using Cli.Instructions.Parsers;
using Microsoft.Extensions.DependencyInjection;

namespace Cli.Instructions.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInstructions(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IConsoleInstructionArgumentBuilder, GuidConsoleInstructionArgumentBuilder>()
            .AddSingleton<IConsoleInstructionArgumentBuilder, StringConsoleInstructionArgumentBuilder>()
            .AddSingleton<IConsoleInstructionArgumentBuilder, IntConsoleInstructionArgumentBuilder>()
            .AddSingleton<IConsoleInstructionArgumentBuilder, DecimalConsoleInstructionArgumentBuilder>()
            .AddSingleton<IConsoleInstructionArgumentBuilder, DateOnlyConsoleInstructionArgumentBuilder>()
            .AddSingleton<IConsoleInstructionArgumentBuilder, BoolConsoleInstructionArgumentBuilder>()
            .AddSingleton<ConsoleInstructionTokenIndexer>()
            .AddSingleton<ConsoleInstructionTokenExtractor>()
            .AddSingleton<ConsoleInstructionParser>();
}