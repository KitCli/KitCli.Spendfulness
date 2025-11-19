using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Exceptions;
using Cli.Instructions.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Cli.Workflow;

// TODO: CLI - I think this abstraction is unnecessary.
public class CliWorkflowCommandProvider(IServiceProvider serviceProvider) : ICliWorkflowCommandProvider
{
    public CliCommand GetCommand(CliInstruction instruction, List<CliCommandProperty> properties)
    {
        if (string.IsNullOrEmpty(instruction.Name))
        {
            throw new NoInstructionException("No instruction entered.");
        }

        if (properties.Count != 0)
        {
            var continuousGenerator = serviceProvider.GetKeyedService<IUnidentifiedContinuousCliCommandGenerator>(instruction.Name);
            if (continuousGenerator == null)
            {
                throw new NoCommandGeneratorException("Did not find generator for " + instruction.Name);
            }
            
            return continuousGenerator.Generate(instruction, properties);
        }

        var generator = serviceProvider.GetKeyedService<IUnidentifiedCliCommandGenerator>(instruction.Name);
        if (generator == null)
        {
            throw new NoCommandGeneratorException("Did not find generator for " + instruction.Name);
        }

        return generator.Generate(instruction);
    }
}