namespace YnabProgressConsole.Compilation;

public interface IGroupViewModelBuilder<in TGroup> : IViewModelBuilder
{
    public IGroupViewModelBuilder<TGroup> AddGroups(IEnumerable<TGroup> groups);
}