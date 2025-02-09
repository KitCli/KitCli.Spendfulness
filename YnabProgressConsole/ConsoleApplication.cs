using MediatR;
using Microsoft.Extensions.DependencyInjection;
using YnabProgressConsole.Commands;
using YnabProgressConsole.Instructions;

namespace YnabProgressConsole;

public class ConsoleApplication(IServiceProvider serviceProvider)
{
    public async Task Run()
    {
        Console.WriteLine("Welcome to YnabProgressConsole!");

        var instructionParser = serviceProvider.GetRequiredService<InstructionParser>();
        var mediator = serviceProvider.GetRequiredService<IMediator>();

        while (true)
        {
            Console.WriteLine("Enter a Command:");

            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please enter a Command:");
                continue;
            }

            var instruction = instructionParser.Parse(input);

            var commandGenerator =
                serviceProvider.GetRequiredKeyedService<ICommandGenerator>(instruction.InstructionName);
            var command = commandGenerator.Generate(instruction.Arguments.ToList());

            var table = await mediator.Send(command);

            Console.WriteLine(table);
        }
    }
}