using YnabCli.Database.Users;

namespace YnabCli.Database.Accounts;

public class AccountAttributes
{
    public int Id { get; set; }
    public Guid YnabAccountId { get; set; }
    public CustomAccountType CustomAccountType { get; set; }
    public decimal InterestRate { get; set; }
    public User User { get; set; } 
}