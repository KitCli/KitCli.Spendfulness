using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Factories;
using Cli.Commands.Abstractions.Handlers;
using Cli.Commands.Abstractions.Outcomes;
using Cli.Instructions.Abstractions;
using Microsoft.Extensions.Options;
using Splitwise.Clients;
using Splitwise.Http;

namespace Splitter.Cli;

public record TestCliCommand : CliCommand
{
    
}

public class TestCliCommandFactory : ICliCommandFactory<TestCliCommand>
{
    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
    {
        return new TestCliCommand();
    }
}

public class TestCliCommandHandler : CliCommandHandler, ICliCommandHandler<TestCliCommand>
{
    private readonly SplitwiseSettings _settings;
    private readonly SplitwiseHttpClientBuilder _clientBuilder;

    public TestCliCommandHandler(IOptions<SplitwiseSettings> settings, SplitwiseHttpClientBuilder clientBuilder)
    {
        _settings = settings.Value;
        _clientBuilder = clientBuilder;
    }

    public async Task<CliCommandOutcome[]> Handle(TestCliCommand command, CancellationToken cancellationToken)
    {
        var clientBuilderWithApiKey = _clientBuilder.WithBearerToken(_settings.ApiKey);

        var userClient = new UserClient(clientBuilderWithApiKey);

        var currentUser = await userClient.GetCurrentUser();

        return OutcomeAs();
    }
}