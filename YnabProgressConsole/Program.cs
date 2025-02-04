using Microsoft.Extensions.DependencyInjection;
using YnabProgress.Extensions;
using YnabProgressConsole;

var serviceProvider = new ServiceCollection()
    .AddYnabProgress()
    .AddSingleton<YnabProgressConsoleApplication>()
    .BuildServiceProvider();

var app = serviceProvider.GetService<YnabProgressConsoleApplication>();

await app.Run(args);

