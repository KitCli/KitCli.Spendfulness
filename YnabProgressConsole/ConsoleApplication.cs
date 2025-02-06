using MediatR;
using Microsoft.Extensions.DependencyInjection;
using YnabProgressConsole.Commands;

namespace YnabProgressConsole;

public class ConsoleApplication(IServiceProvider serviceProvider)
{
    public async Task Run()
    {
        Console.WriteLine("Welcome to YnabProgressConsole!");
        
        var mediator = serviceProvider.GetService<IMediator>();
        if (mediator is null)
        {
            Console.WriteLine("No mediator registered.");
        }
        
        while (true)
        {
            Console.WriteLine("Enter a Command:");
            
            var rawInput = Console.ReadLine();
            if (string.IsNullOrEmpty(rawInput))
            {
                Console.WriteLine("Please enter a Command:");
                continue;
            }
            
            var input = new ConsoleInput(rawInput);
            
            var commandGenerator = serviceProvider.GetKeyedService<ICommandGenerator>(input.CommandName);
            if (commandGenerator is null)
            {
                Console.WriteLine("Invalid Command");
                continue;
            }
            
            var command = commandGenerator.Generate(input.Arguments);
            
            var table = await mediator.Send(command);
            
            Console.WriteLine(table);
        }
    }
    
}