using Ynab.Clients;
using Ynab.Http;
using YnabCli.Database;

namespace YnabCli.Commands;

public class BudgetsClientFactory(UnitOfWork unitOfWork, YnabHttpClientBuilder httpClientBuilder)
{
    public async Task<BudgetsClient> Create()
    {
        var activeUser = await unitOfWork.GetActiveUser();
        if (activeUser == null)
        {
            throw new Exception("No active user");
        }
        
        var ynabApiTokenSetting = activeUser.Settings.GetYnabApiTokenSetting();
        if (ynabApiTokenSetting is null)
        {
            throw new Exception("No ynab api token");
        }
        
        var builder = httpClientBuilder.WithBearerToken(ynabApiTokenSetting.Value);
        
        return new BudgetsClient(builder);
    }
}