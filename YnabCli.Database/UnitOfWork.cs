using Microsoft.EntityFrameworkCore;
using YnabCli.Database.Users;

namespace YnabCli.Database;

public class UnitOfWork(YnabCliDbContext ynabCliDbContext)
{
    public Task<User?> GetActiveUser()
        => ynabCliDbContext
            .Users
            .Include(u => u.Settings)
            .ThenInclude(s => s.Type)
            .FirstOrDefaultAsync(u => u.Active);
}