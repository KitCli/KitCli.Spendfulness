namespace YnabCli.Commands;

public interface ITypedCommandGenerator<TCommand> where TCommand : ICommand
{
    // This is helping us with reflection for DI.
}