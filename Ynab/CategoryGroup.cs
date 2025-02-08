using Ynab.Clients;
using Ynab.Responses.Categories;
using Ynab.Sanitisers;

namespace Ynab;

public class CategoryGroup
{
    private readonly CategoriesClient _categoryClient;
    private readonly CategoryGroupResponse _categoryGroupResponse;

    public string Name => _categoryGroupResponse.Name;

    public CategoryGroup(CategoriesClient categoryClient, CategoryGroupResponse categoryGroupResponse)
    {
        _categoryClient = categoryClient;
        _categoryGroupResponse = categoryGroupResponse;
    }
    
    public IEnumerable<Category> Categories => _categoryGroupResponse.Categories
        .Select(categories => new Category(_categoryClient, categories));
    
    public decimal Balance => _categoryGroupResponse.Categories.Sum(category =>
        MilliunitSanitiser.Calculate(category.Balance));
}