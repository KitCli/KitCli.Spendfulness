namespace YnabCli.Commands.Setting.Create;

public class SettingCreateCommand : ICommand
{
    public static class ArgumentNames
    {
        public const string Type = "type";
        public const string Value = "value";
    }
    
    public string? Type { get; set; }
    public string? Value { get; set; }
}