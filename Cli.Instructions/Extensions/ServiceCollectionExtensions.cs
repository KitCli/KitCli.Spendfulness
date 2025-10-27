using Cli.Instructions.Builders;
using Cli.Instructions.Parsers;
using Microsoft.Extensions.DependencyInjection;

namespace Cli.Instructions.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCliInstructions(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<ICliInstructionArgumentBuilder, GuidCliInstructionArgumentBuilder>()
            .AddSingleton<ICliInstructionArgumentBuilder, StringCliInstructionArgumentBuilder>()
            .AddSingleton<ICliInstructionArgumentBuilder, IntCliInstructionArgumentBuilder>()
            .AddSingleton<ICliInstructionArgumentBuilder, DecimalCliInstructionArgumentBuilder>()
            .AddSingleton<ICliInstructionArgumentBuilder, DateOnlyCliInstructionArgumentBuilder>()
            .AddSingleton<ICliInstructionArgumentBuilder, BoolCliInstructionArgumentBuilder>()
            .AddSingleton<CliInstructionParser>();
}