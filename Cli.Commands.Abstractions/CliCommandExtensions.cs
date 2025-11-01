namespace Cli.Commands.Abstractions;

public static class CliCommandExtensions
{
    /// <summary>
    /// Get the name of the command.
    /// (without the suffix)
    /// </summary>
    /// <param name="command"></param>
    /// <typeparam name="TCliCommand"></typeparam>
    /// <returns></returns>
    public static string GetCommandName<TCliCommand>(this TCliCommand command) where TCliCommand : CliCommand
    {
        var commandSuffix = nameof(CliCommand);
        var commandType = typeof(TCliCommand);
        return commandType.Name.Replace(commandSuffix, string.Empty);
    }
}