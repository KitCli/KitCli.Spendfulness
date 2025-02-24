using Microsoft.EntityFrameworkCore;
using YnabCli.Database.Users;

namespace YnabCli.Database;

public static class DbSetExtensions 
{
    public static async Task<User> FindActiveUserAsync(this DbSet<User> users)
    {
        var activeUser = await users.FirstOrDefaultAsync(u => u.Active);
        if (activeUser == null)
        {
            throw new NullReferenceException("No active user");
        }
        
        return activeUser;
    }
}