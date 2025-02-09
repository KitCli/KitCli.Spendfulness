namespace YnabProgressConsole.Compilation;

public abstract class ViewModelBuilder
{
    protected List<string> ColumnNames = [];
    protected string SortColumnName = string.Empty;
    protected SortOrder SortOrder = SortOrder.Ascending;
    
    public IViewModelBuilder AddColumnNames(params string[] columnNames)
    {
        ColumnNames = columnNames.ToList();
        return GetCurrentBuilder();
    }

    public IViewModelBuilder AddSortColumnName(string columnName)
    {
        SortColumnName = columnName;
        return GetCurrentBuilder();
    }

    public IViewModelBuilder AddSortOrder(SortOrder sortOrder)
    {
        SortOrder = sortOrder;
        return GetCurrentBuilder();
    }

    private IViewModelBuilder GetCurrentBuilder()
    {
        var current = this as IViewModelBuilder;
        if (current is null)
        {
            throw new Exception("Attempted to return a non-IViewModelBuilder superclass of ViewModelBuilder");
        }
        
        return current;
    }
}