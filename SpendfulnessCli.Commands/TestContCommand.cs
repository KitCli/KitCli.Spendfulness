using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Outcomes;
using Cli.Commands.Abstractions.Properties;
using Cli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands;

public record TestContCommand : ContinuousCliCommand
{
    
}

public class TestContCommandGenerator : ICliCommandGenerator<TestContCommand>
{
    public CliCommand Generate(CliInstruction instruction)
    {
        return new TestContCommand();
    }
}

public class TestContCommandHandler : ICliCommandHandler<TestContCommand>
{
    public Task<CliCommandOutcome> Handle(TestContCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine("TestContCommand executed.");
        
        var properties = new List<CliCommandProperty>
        {
            new TestContCommandProperty("test", "value")
        };
        
        var outcome = new CliCommandPropertiesOutcome(properties);
        return Task.FromResult<CliCommandOutcome>(outcome);
    }
}

public class TestContCommandProperty(string propertyKey, string propertyValue) : CustomCliCommandProperty<string>(propertyKey, propertyValue)
{
    
}