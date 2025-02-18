namespace YnabCli.ViewModels.ViewModels;

public class ViewModel
{
    public List<string> Columns { get; set; } = [];
    public List<List<object>> Rows { get; set; } = [];

    public bool ShowRowCount { get; set; } = true;
}