// See https://aka.ms/new-console-template for more information

using Cli;
using Cli.Commands.Abstractions.Extensions;
using Splitter.Cli;
using Splitwise;

Console.WriteLine("Hello, World!");

// var y = new ServiceCollection();
//
// y.AddSplitwise();
//
// var serviceProvider = y.BuildServiceProvider();


var app = new CliAppBuilder()
    .WithCli<SplitterCli>()
    .WithUserSecretsSettings()
    .WithSettings<SplitwiseSettings>()
    .WithCustomServices(services =>services
        .AddSplitwise()
        .AddCommandsFromAssembly(typeof(TestCliCommand).Assembly))
    .Build();

await app.Run();