namespace Cli.Commands.Abstractions.Properties;

public class OutputCliCommandProperty(string output) : ValuedCliCommandProperty<string>(output)
{
}