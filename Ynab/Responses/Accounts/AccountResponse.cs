using System.Text.Json.Serialization;

namespace Ynab.Responses.Accounts;

public class AccountResponse
{
    public int Balance { get; set; }
    
    [JsonPropertyName("on_budget")]
    public bool OnBudget { get; set; }
    
    public bool Closed {get; set; }
}