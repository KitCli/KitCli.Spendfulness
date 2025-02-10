namespace YnabProgressConsole.Compilation.ViewModelBuilders;

public interface IGroupViewModelBuilder<in TGroup> : IViewModelBuilder
{
    public IGroupViewModelBuilder<TGroup> AddGroups(IEnumerable<TGroup> groups);
}