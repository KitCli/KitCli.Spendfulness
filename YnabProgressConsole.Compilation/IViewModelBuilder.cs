namespace YnabProgressConsole.Compilation;

public interface IViewModelBuilder
{
    public IViewModelBuilder AddColumnNames(params string[] columnNames);
    
    public IViewModelBuilder AddSortColumnName(string columnName);
    
    public IViewModelBuilder AddSortOrder(SortOrder sortOrder);

    public ViewModel Build();
}