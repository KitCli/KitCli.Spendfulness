using MediatR;

namespace YnabProgressConsole;

public class YnabProgressConsoleApplication
{
    private readonly IMediator _mediator;

    public YnabProgressConsoleApplication(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Run(params string[] args)
    {
        // COMMANDS LIKE: /command-name-might-be somethig like 1 2 3
        
    }
}