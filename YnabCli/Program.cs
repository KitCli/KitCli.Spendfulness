using Microsoft.Extensions.DependencyInjection;
using Ynab.Extensions;
using YnabCli;
using YnabCli.Commands;
using YnabCli.Database;
using YnabCli.Instructions;
using YnabCli.ViewModels.Extensions;

var serviceProvider = new ServiceCollection()
    .AddYnab() // Speak to the YNAB API
    .AddConsoleCompilation() // Compile into ConsoleTables.
    .AddConsoleCommands() // Convert them into MediatR requests
    .AddConsoleInstructions() // Understand terminal commands
    .AddYnabCliDb() // Store data
    .AddSingleton<ConsoleApplication>() // Front-end
    .BuildServiceProvider();

var app = serviceProvider.GetRequiredService<ConsoleApplication>();

 await app.Run();

