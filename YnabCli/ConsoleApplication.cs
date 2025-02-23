using MediatR;
using Microsoft.Extensions.DependencyInjection;
using YnabCli.Commands;
using YnabCli.Commands.Clear;
using YnabCli.Commands.Exit;
using YnabCli.Instructions.Extraction;
using YnabCli.Instructions.Parsers;

namespace YnabCli;

public class ConsoleApplication(IServiceProvider serviceProvider)
{
    public async Task Run()
    {
        PrintToConsole("Welcome to YnabCli!");

        var instructionTokenExtractor = serviceProvider.GetRequiredService<InstructionTokenExtractor>();
        var instructionParser = serviceProvider.GetRequiredService<InstructionParser>();
        var mediator = serviceProvider.GetRequiredService<IMediator>();

        var noExitCommand = true;

        while (noExitCommand)
        {
            PrintToConsole("Enter a Command:");

            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                PrintToConsole("Please enter a Command:");
                continue;
            }

            var tokens = instructionTokenExtractor.Extract(input);
            if (tokens.NameToken is ExitCommand.CommandName or ExitCommand.ShorthandCommandName)
            {
                PrintToConsole("Exiting...");
                noExitCommand = false;
                continue;
            }
            
            if (tokens.NameToken is ClearCommand.CommandName or ClearCommand.ShorthandCommandName)
            {
                ClearTheConsole();
                continue;
            }
            
            var instruction = instructionParser.Parse(tokens);
            
            var generator = serviceProvider.GetKeyedService<ICommandGenerator>(instruction.Name);
            if (generator == null)
            {
                PrintToConsole("Invalid Command, Try Again...");
                continue;
            }
            
            var command = generator.Generate(instruction.SubName, instruction.Arguments.ToList());
            
            var table = await mediator.Send(command);

            PrintToConsole(table.ToString());
        }
        
        // Function never returns because... true is always true.
        // TODO: Exit While loop if command was exist command.
    }

    private static void PrintToConsole(string print)
    {
        Console.WriteLine(print);
        Console.WriteLine();
    }
    
    public static void ClearTheConsole() => Console.Clear();
}