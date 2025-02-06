using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Ynab.Extensions;
using YnabProgressConsole;
using YnabProgressConsole.Commands;
using YnabProgressConsole.Commands.CommandList;
using YnabProgressConsole.Commands.RecurringTransactions;

var serviceProvider = new ServiceCollection()
    .AddYnab()
    .AddMediatR(cfg => 
        cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
    .AddKeyedSingleton<ICommandGenerator, CommandListCommandGenerator>(CommandListCommandGenerator.CommandName)
    .AddKeyedSingleton<ICommandGenerator, RecurringTransactionsCommandGenerator>(RecurringTransactionsCommand.CommandName)
    .AddSingleton<ConsoleApplication>()
    .BuildServiceProvider();

var app = serviceProvider.GetService<ConsoleApplication>();

 await app.Run();

