using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Exceptions;
using Cli.Instructions.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Cli.Workflow;

public class CliWorkflowCommandProvider(IServiceProvider serviceProvider)
{
    public ICliCommand GetCommand(ConsoleInstruction instruction)
    {
        if (string.IsNullOrEmpty(instruction.Name))
        {
            throw new NoInstructionException("No instruction entered.");
        }
        
        var generator = serviceProvider.GetKeyedService<IGenericCommandGenerator>(instruction.Name);
        if (generator == null)
        {
            throw new NoCommandGeneratorException("Did not find generator for " + instruction.Name);
        }

        return generator.Generate(instruction.SubName, instruction.Arguments.ToList());
    }
}